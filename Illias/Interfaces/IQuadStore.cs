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
using System.Collections.Generic;

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
    public interface IQuadStore<TSystemId, TQuadId, TTransactionId, TSPO, TContext>

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPO           : IEquatable<TSPO>,           IComparable<TSPO>,           IComparable
        where TContext       : IEquatable<TContext>,       IComparable<TContext>,       IComparable

    {

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
        Transaction<TTransactionId, TSystemId>
            BeginTransaction(String         Name             = "",
                             Boolean        Distributed      = false,
                             Boolean        LongRunning      = false,
                             IsolationLevel IsolationLevel   = IsolationLevel.Write,
                             DateTime?      CreationTime     = null,
                             DateTime?      InvalidationTime = null);
        
        #endregion

        #region Add(...)

        /// <summary>
        /// Adds a new quad based on the given parameters to the QuadStore.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>A new quad based on the given parameters.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
            Add(TSPO     Subject,
                TSPO     Predicate,
                TSPO     Object,
                TContext Context = default(TContext),
                Boolean  Connect = true);

        #endregion

        #region Get(...)

        /// <summary>
        /// Returns the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad having the given QuadId.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
            GetQuad(TQuadId QuadId);


        /// <summary>
        /// Returns all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <returns>All quads matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
            GetQuads(TSPO     Subject,
                     TSPO     Predicate,
                     TSPO     Object,
                     TContext Context = default(TContext));


        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selcting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selcting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selcting objects.</param>
        /// <param name="ContextOrGraphSelector">A delegate for selcting contexts or graphs.</param>
        /// <returns>An enumeration of selected Quads.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
            GetQuads(SubjectSelector<TSPO> SubjectSelector = null,
                     PredicateSelector<TSPO>      PredicateSelector      = null,
                     ObjectSelector<TSPO>         ObjectSelector         = null,
                     ContextSelector<TContext> ContextOrGraphSelector = null);

        #endregion

        #region Remove(...)

        /// <summary>
        /// Removes the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad after removal having the given QuadId.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>
            Remove(TQuadId QuadId);

        /// <summary>
        /// Removes all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The Subject.</param>
        /// <param name="Predicate">The Predicate.</param>
        /// <param name="Object">The Object.</param>
        /// <param name="Context">The Context.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
            Remove(TSPO     Subject,
                   TSPO     Predicate,
                   TSPO     Object,
                   TContext Context = default(TContext));

        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selcting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selcting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selcting objects.</param>
        /// <param name="ContextSelector">A delegate for selcting contexts.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext>>
            Remove(SubjectSelector<TSPO>     SubjectSelector   = null,
                   PredicateSelector<TSPO> PredicateSelector = null,
                   ObjectSelector<TSPO>       ObjectSelector    = null,
                   ContextSelector<TContext>     ContextSelector   = null);

        #endregion

        #region Utils

        /// <summary>
        /// Update all references of the given Quad to provide an index-free adjacency.
        /// </summary>
        /// <param name="Quad">A quad.</param>
        void UpdateReferences(IQuad<TSystemId, TQuadId, TTransactionId, TSPO, TContext> Quad);

        #endregion

    }

}
