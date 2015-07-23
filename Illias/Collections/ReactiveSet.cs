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
    public class ReactiveSet<T> : IEnumerable<T>,
                                  IEquatable<ReactiveSet<T>>
    {

        #region Data

        private readonly HashSet<T> _Set;

        #endregion

        #region Properties

        #region Count

        /// <summary>
        /// The number of items stored within this reactive set.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                return (UInt64) _Set.Count;
            }
        }

        #endregion

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

        /// <summary>
        /// Create a new reactive set storing on the given items.
        /// </summary>
        /// <param name="Items">An optional enumeration of items to store.</param>
        public ReactiveSet(IEnumerable<T> Items = null)
        {

            if (Items == null)
                this._Set = new HashSet<T>();

            else
                this._Set = new HashSet<T>(Items);

        }

        #endregion


        #region Add(Items...)

        /// <summary>
        /// Add the given array of items to the reactive set.
        /// </summary>
        /// <param name="Items">An array of items.</param>
        public ReactiveSet<T> Add(params T[] Items)
        {
            return Add(Items as IEnumerable<T>);
        }

        #endregion

        #region Add(Items)

        /// <summary>
        /// Add the given enumeration of items to the reactive set.
        /// </summary>
        /// <param name="Items">An enumeration of items.</param>
        public ReactiveSet<T> Add(IEnumerable<T> Items)
        {

            if (Items != null)
            {

                var OnItemAddedLocal  = OnItemAdded;
                var Timestamp         = DateTime.Now;

                Items.ForEach(Item => {

                    _Set.Add(Item);

                    if (OnItemAddedLocal != null)
                        OnItemAddedLocal(Timestamp, this, Item);

                });

            }

            return this;

        }

        #endregion

        #region Contains(Item)

        /// <summary>
        /// Determines whether the reactive set contains the given item.
        /// </summary>
        /// <param name="Item">An item.</param>
        /// <returns>true if the reactive set contains the specified item; otherwise, false.</returns>
        public Boolean Contains(T Item)
        {
            return _Set.Contains(Item);
        }

        #endregion

        #region Remove(Items...)

        /// <summary>
        /// Remove the given array of items from the reactive set.
        /// </summary>
        /// <param name="Items">An array of items.</param>
        public ReactiveSet<T> Remove(params T[] Items)
        {
            return Remove(Items as IEnumerable<T>);
        }

        #endregion

        #region Remove(Items)

        /// <summary>
        /// Remove the given enumeration of items from the reactive set.
        /// </summary>
        /// <param name="Items">An enumeration of items.</param>
        public ReactiveSet<T> Remove(IEnumerable<T> Items)
        {

            if (Items != null)
            {

                var OnItemRemovedLocal  = OnItemRemoved;
                var Timestamp           = DateTime.Now;

                Items.ForEach(Item => {

                    _Set.Remove(Item);

                    if (OnItemRemovedLocal != null)
                        OnItemRemovedLocal(Timestamp, this, Item);

                });

            }

            return this;

        }

        #endregion


        #region IEnumerable<T> Members

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

        #region IEquatable<ReactiveSet<T>> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is a reactive set.
            var ReactiveSet = Object as ReactiveSet<T>;
            if ((Object) ReactiveSet == null)
                return false;

            return this.Equals(ReactiveSet);

        }

        #endregion

        #region Equals(ReactiveSet)

        /// <summary>
        /// Compares two reactive sets for equality.
        /// </summary>
        /// <param name="ReactiveSet">A reactive set to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ReactiveSet<T> ReactiveSet)
        {

            if ((Object) ReactiveSet == null)
                return false;

            if (this.Count != ReactiveSet.Count)
                return false;

            return _Set.All(item => ReactiveSet.Contains(item));

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            return _Set.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Get a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "'" + _Set.AggregateWith(", ") + "'";
        }

        #endregion

    }

}
