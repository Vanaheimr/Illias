﻿/*
 * Copyright (c) 2010-2016 Achim 'ahzf' Friedland <achim.friedland@graphdefined.com>
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
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// Extension methods for the IDictionary interface.
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

        #region AddAndReturnDictionary(this Dictionary, KeyCreator, V)

        /// <summary>
        /// Another way to add an element to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="KeyCreator">A delegate providing the key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The changed dictionary.</returns>
        public static IDictionary<K, V> AddAndReturnDictionary<K, V>(this IDictionary<K, V> Dictionary, Func<V, K> KeyCreator, V Value)
        {

            if (KeyCreator != null)
                throw new ArgumentNullException("The given delegate msut not be null!", "KeyCreator");

            Dictionary.Add(KeyCreator(Value), Value);

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

        #region AddAndReturnKeyValuePair(this Dictionary, KeyCreator, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="KeyCreator">A delegate providing the key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element as key value pair.</returns>
        public static KeyValuePair<K, V> AddAndReturnKeyValuePair<K, V>(this IDictionary<K, V> Dictionary, Func<V, K> KeyCreator, V Value)
        {

            if (KeyCreator != null)
                throw new ArgumentNullException("The given delegate msut not be null!", "KeyCreator");

            var KeyValuePair = new KeyValuePair<K, V>(KeyCreator(Value), Value);

            Dictionary.Add(KeyValuePair);

            return KeyValuePair;

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

        #region AddAndReturnKey(this Dictionary, KeyCreator, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="KeyCreator">A delegate providing the key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element key.</returns>
        public static K AddAndReturnKey<K, V>(this IDictionary<K, V> Dictionary, Func<V, K> KeyCreator, V Value)
        {

            if (KeyCreator != null)
                throw new ArgumentNullException("The given delegate msut not be null!", "KeyCreator");

            // Just call it once... as you never know what happens when you do it twice ;)!
            var Key = KeyCreator(Value);
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

        #region AddAndReturnValue(this Dictionary, KeyCreator, V)

        /// <summary>
        /// Another way to add an value to a dictionary.
        /// </summary>
        /// <param name="Dictionary">A dictionary.</param>
        /// <param name="KeyCreator">A delegate providing the key.</param>
        /// <param name="Value">The element value.</param>
        /// <returns>The element value.</returns>
        public static V AddAndReturnValue<K, V>(this IDictionary<K, V> Dictionary, Func<V, K> KeyCreator, V Value)
        {

            if (KeyCreator != null)
                throw new ArgumentNullException("The given delegate msut not be null!", "KeyCreator");

            Dictionary.Add(KeyCreator(Value), Value);

            return Value;

        }

        #endregion

    }

}
