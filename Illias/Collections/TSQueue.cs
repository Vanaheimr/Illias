/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Illias <http://www.github.com/Vanaheimr/Illias>
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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias.Collections
{

    /// <summary>
    /// A thread-safe, lock-free queue.
    /// </summary>
    /// <typeparam name="T">The type of the values stored within the queue.</typeparam>
    public class TSQueue<T> : IEnumerable<T>
    {

        #region (class) QueueElement

        /// <summary>
        /// An element within a queue.
        /// </summary>
        public class QueueElement
        {

            #region Properties

            /// <summary>
            /// Return the next element within the queue.
            /// </summary>
            public QueueElement Next  { get; set; }

            /// <summary>
            /// Return the value stored within the element.
            /// </summary>
            public T            Value { get; private set; }

            #endregion

            #region Constructor(s)

            #region QueueElement(Value)

            /// <summary>
            /// Create a single queue element.
            /// </summary>
            /// <param name="Value">The value stored within the node.</param>
            public QueueElement(T Value)
            {
                this.Value = Value;
                this.Next  = null;
            }

            #endregion

            #endregion

        }

        #endregion


        #region Properties

        #region First

        private QueueElement _FirstQueueElement;

        /// <summary>
        /// The first element of the queue.
        /// </summary>
        public QueueElement First
        {
            get
            {
                return _FirstQueueElement;
            }
        }

        #endregion

        #region MaxNumberOfElements

        private UInt64 _MaxNumberOfElements;

        /// <summary>
        /// The maximal number of values within the queue.
        /// RemoveOldestQueueElement() will be called to remove dispensable elements.
        /// </summary>
        public UInt64 MaxNumberOfElements
        {

            get
            {
                return _MaxNumberOfElements;
            }

            set
            {
                if (value < Int32.MaxValue)
                    _MaxNumberOfElements = value;
            }

        }

        #endregion

        #region Count

        private Int64 _Count;

        /// <summary>
        /// The current number of elements within the queue.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                return (UInt64) _Count;
            }
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// A delegate called whenever an element was added to or removed from the queue.
        /// </summary>
        /// <param name="Value"></param>
        public delegate void QueueDelegate(TSQueue<T> Sender, T Value);

        /// <summary>
        /// Called whenever an element was added to the queue.
        /// </summary>
        public event QueueDelegate OnAdded;

        /// <summary>
        /// Called whenever an element of the queue was removed.
        /// </summary>
        public event QueueDelegate OnRemoved;

        #endregion

        #region Constructor(s)

        #region TSQueue(MaxNumberOfElements = 100)

        /// <summary>
        /// Create a new thread-safe, lock-free queue.
        /// </summary>
        /// <param name="MaxNumberOfElements">The maximal number of values within the queue.</param>
        public TSQueue(UInt64 MaxNumberOfElements = 100)
        {
            this.MaxNumberOfElements = MaxNumberOfElements;
        }

        #endregion

        #endregion


        #region Push(Value)

        /// <summary>
        /// Push a new value into the queue.
        /// </summary>
        /// <param name="Value">The value.</param>
        public QueueElement Push(T Value)
        {

            var NewQueueElement = new QueueElement(Value);

            QueueElement OldFirst;

            do
            {
                OldFirst              = First;
                NewQueueElement.Next  = OldFirst;
            }
            while (Interlocked.CompareExchange<QueueElement>(ref _FirstQueueElement, NewQueueElement, OldFirst) != OldFirst);

            Interlocked.Increment(ref _Count);

            // Multiple concurrent threads might remove more than expected!
            // But it is assumed, that this is not a problem to anyone.
            while ((UInt64) _Count > _MaxNumberOfElements)
                Pop();

            var _OnAdded = OnAdded;
            if (_OnAdded != null)
                _OnAdded(this, Value);

            return NewQueueElement;

        }

        #endregion

        #region Peek()

        /// <summary>
        /// Return the oldest value of the queue without removing it.
        /// </summary>
        public T Peek()
        {
            return _FirstQueueElement.Value;
        }

        #endregion

        #region Pop()

        /// <summary>
        /// Return the oldest value of the queue and remove it.
        /// </summary>
        public T Pop()
        {

            QueueElement Oldest     = _FirstQueueElement;
            QueueElement PreOldest  = null;

            while (Oldest.Next != null)
            {
                PreOldest  = Oldest;
                Oldest     = Oldest.Next;
            }

            if (PreOldest != null)
            {
                PreOldest.Next = null;
                Interlocked.Decrement(ref _Count);
            }

            var _OnRemoved = OnRemoved;
            if (_OnRemoved != null)
                _OnRemoved(this, _FirstQueueElement.Value);

            return Oldest.Value;

        }

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Get an enumerator for the queue..
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {

            var node = _FirstQueueElement;

            if (node != null)
            {
                do
                {
                    yield return node.Value;
                } while ((node = node.Next) != null);
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}
