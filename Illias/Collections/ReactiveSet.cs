/*
 * Copyright (c) 2010-2015 Achim 'ahzf' Friedland <achim@graphdefined.org>
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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// A reactive set.
    /// </summary>
    public class ReactiveSet<T> : IEnumerable<T>
    {

        #region Data

        private readonly HashSet<T> _Set;

        #endregion

        #region Properties

        #endregion

        #region Events

        #region OnItemAdded

        /// <summary>
        /// A delegate called whenever the aggregated dynamic status of all subordinated EVSEs changed.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        public delegate void OnItemAddedDelegate(DateTime Timestamp, ReactiveSet<T> ReactiveSet, T Item);

        /// <summary>
        /// An event fired whenever the aggregated dynamic status of all subordinated EVSEs changed.
        /// </summary>
        public event OnItemAddedDelegate OnItemAdded;

        #endregion

        #region OnItemRemoved

        /// <summary>
        /// A delegate called whenever the aggregated dynamic status of all subordinated EVSEs changed.
        /// </summary>
        /// <param name="Timestamp">The timestamp when this change was detected.</param>
        public delegate void OnItemRemovedDelegate(DateTime Timestamp, ReactiveSet<T> ReactiveSet, T Item);

        /// <summary>
        /// An event fired whenever the aggregated dynamic status of all subordinated EVSEs changed.
        /// </summary>
        public event OnItemRemovedDelegate OnItemRemoved;

        #endregion

        #endregion

        #region Constructor(s)

        public ReactiveSet()
        {

            this._Set = new HashSet<T>();

        }

        #endregion


        public ReactiveSet<T> Add(T Item)
        {

            _Set.Add(Item);

            var OnItemAddedLocal = OnItemAdded;
            if (OnItemAddedLocal != null)
                OnItemAddedLocal(DateTime.Now, this, Item);

            return this;

        }

        public ReactiveSet<T> Add(IEnumerable<T> Items)
        {

            var OnItemAddedLocal = OnItemAdded;

            Items.ForEach(Item => {

                _Set.Add(Item);

                if (OnItemAddedLocal != null)
                    OnItemAddedLocal(DateTime.Now, this, Item);

            });

            return this;

        }

        public Boolean Contains(T Item)
        {
            return _Set.Contains(Item);
        }


        public ReactiveSet<T> Remove(T Item)
        {

            _Set.Remove(Item);

            var OnItemRemovedLocal = OnItemRemoved;
            if (OnItemRemovedLocal != null)
                OnItemRemovedLocal(DateTime.Now, this, Item);

            return this;

        }

        public ReactiveSet<T> Remove(IEnumerable<T> Items)
        {

            var OnItemRemovedLocal = OnItemRemoved;

            Items.ForEach(Item => {

                _Set.Remove(Item);

                if (OnItemRemovedLocal != null)
                    OnItemRemovedLocal(DateTime.Now, this, Item);

            });

            return this;

        }


        #region GetEnumerator()

        /// <summary>
        /// Enumerate the reactive list.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _Set.GetEnumerator();
        }

        /// <summary>
        /// Enumerate the reactive list.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _Set.GetEnumerator();
        }

        #endregion

    }

}
