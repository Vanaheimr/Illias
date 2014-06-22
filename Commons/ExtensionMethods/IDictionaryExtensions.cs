/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Illias Commons <http://www.github.com/Vanaheimr/Illias>
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

namespace eu.Vanaheimr.Illias.Commons
{

    /// <summary>
    /// Extension methods for the IDictionaryExtensions interface.
    /// </summary>
    public static class IDictionaryExtensions
    {

        #region AddAndReturnDictionary(this Dictionary, K, V)

        /// <summary>
        /// Another way to add an element to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="Key">The element key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The changed dictionary.</returns>
        public static IDictionary<K, V> AddAndReturnDictionary<K, V>(this IDictionary<K, V> Dictionary, K Key, V Value)
        {
            Dictionary.Add(Key, Value);
            return Dictionary;
        }

        #endregion

        #region AddAndReturnKeyValuePair(this Dictionary, K, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="Key">The element key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element as key value pair.</returns>
        public static KeyValuePair<K, V> AddAndReturnKeyValuePair<K, V>(this IDictionary<K, V> Dictionary, K Key, V Value)
        {
            Dictionary.Add(Key, Value);
            return new KeyValuePair<K,V>(Key, Value);
        }

        #endregion

        #region AddAndReturnKey(this Dictionary, K, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="Key">The element key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element key.</returns>
        public static K AddAndReturnKey<K, V>(this IDictionary<K, V> Dictionary, K Key, V Value)
        {
            Dictionary.Add(Key, Value);
            return Key;
        }

        #endregion

        #region AddAndReturnValue(this Dictionary, K, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="Key">The element key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element value.</returns>
        public static V AddAndReturnValue<K, V>(this IDictionary<K, V> Dictionary, K Key, V Value)
        {
            Dictionary.Add(Key, Value);
            return Value;
        }

        #endregion

    }

}
