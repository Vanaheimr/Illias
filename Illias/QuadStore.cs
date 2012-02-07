/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Illias <http://www.github.com/ahzf/Illias>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

#endregion

namespace de.ahzf.Illias
{

    /// <summary>
    /// A QuadStore stores little fragments of a graph called quads.
    /// Subject -Predicate-> Object [Context]
    /// Vertex  -Edge->      Vertex [HyperEdge]
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPO">The type of the subjects, predicates and objects.</typeparam>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class QuadStore<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
                     : IQuadStore<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
        
        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPO           : IEquatable<TSPO>,           IComparable<TSPO>,           IComparable
        where TContext       : IEquatable<TContext>,       IComparable<TContext>,       IComparable

    {

        #region Data

        private TSystemId               _SystemId;
        private Int64                   _CurrentQuadId;
        private QuadIdConverterDelegate _QuadIdConverter;
        private DefaultContextDelegate  _DefaultContext;

        #region Indices for the Subject, Predicate, Object and Context

        // Maybe look for better data structures in the future.
        private ConcurrentDictionary<TQuadId,       IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>  _QuadIdIndex;
        private ConcurrentDictionary<TSPO,     List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>> _SubjectIndex;
        private ConcurrentDictionary<TSPO,     List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>> _PredicateIndex;
        private ConcurrentDictionary<TSPO,     List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>> _ObjectIndex;
        private ConcurrentDictionary<TContext, List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>> _ContextIndex;

        #endregion

        private static ThreadLocal<Transaction<TTransactionId, TSystemId>> _ThreadLocalTransaction;

        #endregion

        #region Delegates

        /// <summary>
        /// A delegate to convert a QuadId from the internal
        /// Int64 representation to the actual type T of a quad.
        /// </summary>
        /// <param name="QuadId">A QuadId.</param>
        /// <returns>A QuadId of type T.</returns>
        public delegate TQuadId QuadIdConverterDelegate(Int64 QuadId);

        /// <summary>
        /// A delegate returning the default context of a quad
        /// if none was given.
        /// </summary>
        public delegate TContext DefaultContextDelegate();

        #endregion

        #region Constructor(s)

        #region QuadStore(SystemId, QuadIdConverter, DefaultContext)

        /// <summary>
        /// Creates a new QuadStore storing little fragments of a graph called quads.
        /// Subject -Predicate-> Object [Context]
        /// Vertex  -Edge->      Vertex [HyperEdge]
        /// </summary>
        /// <param name="SystemId">The SystemId for this QuadStore.</param>
        /// <param name="QuadIdConverter">A delegate to convert a QuadId from the internal Int64 representation to the actual type T of a quad.</param>
        /// <param name="DefaultContext">The default context of a quad if none was given.</param>
        public QuadStore(TSystemId SystemId, QuadIdConverterDelegate QuadIdConverter, DefaultContextDelegate DefaultContext)
        {

            #region Initial checks

            if (SystemId == null || SystemId.Equals(default(TSystemId)))
                throw new ArgumentNullException("The SystemId must not be null or default(TSystemId)!");

            if (QuadIdConverter == null)
                throw new ArgumentNullException("The QuadIdConverter must not be null!");

            if (DefaultContext == null)
                throw new ArgumentNullException("The DefaultContext must not be null!");

            #endregion

            this._SystemId        = SystemId;
            this._QuadIdConverter = QuadIdConverter;
            this._DefaultContext  = DefaultContext;

            this._QuadIdIndex     = new ConcurrentDictionary<TQuadId,         IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>> ();
            this._SubjectIndex    = new ConcurrentDictionary<TSPO,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>>();
            this._PredicateIndex  = new ConcurrentDictionary<TSPO, List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>>();
            this._ObjectIndex     = new ConcurrentDictionary<TSPO,    List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>>();
            this._ContextIndex    = new ConcurrentDictionary<TContext,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>>();

        }

        #endregion

        #endregion


        #region BeginTransaction(...)

        /// <summary>
        /// Start a new transaction.
        /// </summary>
        /// <param name="Name">A name or identification for this transaction.</param>
        /// <param name="Distributed">Indicates that this transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that this transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of this transaction.</param>
        /// <param name="CreationTime">The timestamp when this transaction started.</param>
        /// <param name="InvalidationTime">The timestamp when this transaction will be invalid.</param>
        /// <returns>A new transaction object.</returns>
        public Transaction<TTransactionId, TSystemId>
            BeginTransaction(String         Name             = "", 
                             Boolean        Distributed      = false,
                             Boolean        LongRunning      = false,
                             IsolationLevel IsolationLevel   = IsolationLevel.Write,
                             DateTime?      CreationTime     = null,
                             DateTime?      InvalidationTime = null)
        {

            if (_ThreadLocalTransaction != null)
                if (_ThreadLocalTransaction.IsValueCreated)
                {
                    if (_ThreadLocalTransaction.Value.State == TransactionState.Running ||
                        _ThreadLocalTransaction.Value.State == TransactionState.NestedTransaction ||
                        _ThreadLocalTransaction.Value.State == TransactionState.Committing ||
                        _ThreadLocalTransaction.Value.State == TransactionState.RollingBack)
                    {
                        throw new CouldNotBeginTransactionException<TTransactionId, TSystemId>(_ThreadLocalTransaction.Value,
                                                                                               Message: "Transaction still in state '" + _ThreadLocalTransaction.Value.State.ToString() +
                                                                                                        "' on Thread " + Thread.CurrentThread.ManagedThreadId + "!");
                    }
                }

            _ThreadLocalTransaction = new ThreadLocal<Transaction<TTransactionId, TSystemId>>(() => new Transaction<TTransactionId, TSystemId>(default(TTransactionId), _SystemId, Name, Distributed, LongRunning, IsolationLevel, CreationTime, InvalidationTime));

            return _ThreadLocalTransaction.Value;

        }

        #endregion


        #region Add(...)

        #region (private) Add(NewQuad, Connect = true)

        /// <summary>
        /// Adds the given quad to the QuadStore.
        /// </summary>
        /// <param name="NewQuad">The quad to add to the store.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>The given quad.</returns>
        private IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
            Add(IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext> NewQuad,
                Boolean Connect = true)
        {

            #region Initial checks

            if (NewQuad == null)
                throw new ArgumentNullException("The NewQuad must not be null!");

            #endregion

            #region Add quad to Subject, Predicate, Object and Context indices

            List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>> _QuadList = null;

            // Add to QuadId index
            if (_QuadIdIndex.ContainsKey(NewQuad.QuadId) || !_QuadIdIndex.TryAdd(NewQuad.QuadId, NewQuad))
                    throw new AddToQuadIdIndexException<TSystemId, TQuadId, TTransactionId, TSPO, TContext>(NewQuad);

            // Add to Subject index
            if (_SubjectIndex.TryGetValue(NewQuad.Subject, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_SubjectIndex.TryAdd(NewQuad.Subject, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { NewQuad }))
                    throw new AddToSubjectIndexException<TSystemId, TQuadId, TTransactionId, TSPO, TContext>(NewQuad);

            // Add to Predicate index
            if (_PredicateIndex.TryGetValue(NewQuad.Predicate, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_PredicateIndex.TryAdd(NewQuad.Predicate, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { NewQuad }))
                    throw new AddToPredicateIndexException<TSystemId, TQuadId, TTransactionId, TSPO, TContext>(NewQuad);

            // Add to Object index
            if (_ObjectIndex.TryGetValue(NewQuad.Object, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_ObjectIndex.TryAdd(NewQuad.Object, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { NewQuad }))
                    throw new AddToObjectIndexException<TSystemId, TQuadId, TTransactionId, TSPO, TContext>(NewQuad);

            // Add to Context index
            if (_ContextIndex.TryGetValue(NewQuad.Context, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_ContextIndex.TryAdd(NewQuad.Context, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { NewQuad }))
                    throw new AddToContextIndexException<TSystemId, TQuadId, TTransactionId, TSPO, TContext>(NewQuad);

            #endregion

            // Connect this quad to other quads in order
            // to achieve an index-free adjacency
            if (Connect)
                UpdateReferences(NewQuad);

            return NewQuad;

        }

        #endregion

        #region Add(Subject, Predicate, Object, Context = default(T), Connect = true)

        /// <summary>
        /// Adds a new quad based on the given parameters to the QuadStore.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>A new quad based on the given parameters.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
                   Add(TSPO   Subject,
                       TSPO Predicate,
                       TSPO    Object,
                       TContext   Context = default(TContext),
                       Boolean    Connect = true)
        {

            #region Initial checks

            if (Subject   == null || Subject.Equals(default(TSPO)))
                throw new ArgumentNullException("The Subject must not be null or default(T)!");

            if (Predicate == null || Predicate.Equals(default(TSPO)))
                throw new ArgumentNullException("The Predicate must not be null or default(T)!");

            if (Object    == null || Object.Equals(default(TSPO)))
                throw new ArgumentNullException("The Object must not be null or default(T)!");

            if (Context   == null || Context.Equals(default(TContext)))
                Context = _DefaultContext();

            #endregion

            // Calculate a new QuadId.
            Interlocked.Increment(ref _CurrentQuadId);

            // Create a new quad...
            var _Quad = new Quad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
                                 (SystemId:      _SystemId,
                                  TransactionId: default(TTransactionId),
                                  QuadId:        _QuadIdConverter(_CurrentQuadId),
                                  Subject:       Subject,
                                  Predicate:     Predicate,
                                  Object:        Object,
                                  Context:       Context);

            // ...and add it to the store.
            return Add(_Quad, Connect);

        }

        #endregion

        #endregion


        #region Get(...)

        #region GetQuad(QuadId)

        /// <summary>
        /// Returns the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad having the given QuadId.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
                   GetQuad(TQuadId QuadId)
        {

            IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext> _Quad = null;

            if (_QuadIdIndex.TryGetValue(QuadId, out _Quad))
                return _Quad;

            return null;

        }

        #endregion

        #region GetQuads(Subject, Predicate, Object, ContextOrGraph = default(T))

        /// <summary>
        /// Returns all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <returns>All quads matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
                   GetQuads(TSPO     Subject,
                            TSPO     Predicate,
                            TSPO     Object,
                            TContext Context = default(TContext))
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetQuads(SubjectSelector = null, PredicateSelector = null, ObjectSelector = null, ContextOrGraphSelector = null)

        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selcting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selcting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selcting objects.</param>
        /// <param name="ContextOrGraphSelector">A delegate for selcting contexts or graphs.</param>
        /// <returns>An enumeration of selected Quads.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
                   GetQuads(SubjectSelector<TSPO>     SubjectSelector        = null,
                            PredicateSelector<TSPO> PredicateSelector      = null,
                            ObjectSelector<TSPO>       ObjectSelector         = null,
                            ContextSelector<TContext>     ContextOrGraphSelector = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        #region Remove(...)

        #region Remove(QuadId)

        /// <summary>
        /// Removes the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad after removal having the given QuadId.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext> Remove(TQuadId QuadId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(Subject, Predicate, Object, Context = default(T))

        /// <summary>
        /// Removes all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
                   Remove(TSPO   Subject,
                          TSPO Predicate,
                          TSPO    Object,
                          TContext   Context = default(TContext))
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(SubjectSelector = null, PredicateSelector = null, ObjectSelector = null, ContextSelector = null)

        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selcting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selcting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selcting objects.</param>
        /// <param name="ContextSelector">A delegate for selcting contexts.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
                   Remove(SubjectSelector<TSPO>     SubjectSelector   = null,
                          PredicateSelector<TSPO>   PredicateSelector = null,
                          ObjectSelector<TSPO>      ObjectSelector    = null,
                          ContextSelector<TContext> ContextSelector   = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        #region UpdateReferences(NewQuad)

        /// <summary>
        /// Connect this quad to other quads in order to achieve an index-free adjacency.
        /// </summary>
        /// <param name="Quad">A quad to connect to its friends.</param>
        public void UpdateReferences(IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext> Quad)
        {

            List<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>> _QuadList = null;

            // Look for other quads having this Subject as an Object.
            // This means: Which quads want to be connected with me.
            //             Friends.Object -> me.Subject
            if (_ObjectIndex.TryGetValue(Quad.Subject, out _QuadList))
                foreach (var _Friends in _QuadList)
                    if (_Friends.ObjectReference == null)
                        _Friends.ObjectReference = new HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { Quad };
                    else
                        _Friends.ObjectReference.Add(Quad);

            // Look for other quads having this Object as a Subject.
            // This means: To which quads I want to be connected to.
            //             me.Object -> Friends.Subject
            if (_SubjectIndex.TryGetValue(Quad.Object, out _QuadList))
                foreach (var _Quad in _QuadList)
                    if (Quad.ObjectReference == null)
                        Quad.ObjectReference = new HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>() { _Quad };
                    else
                        Quad.ObjectReference.Add(_Quad);

        }

        #endregion

    }

}
