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

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    #region Timestamped<T>

    /// <summary>
    /// A value with its creation timestamp.
    /// </summary>
    /// <typeparam name="T">The type of the timestamped value.</typeparam>
    public struct Timestamped<T>
    {

        #region Properties

        #region Timestamp

        private readonly DateTime _Timestamp;

        /// <summary>
        /// The timestamp of the value creation.
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
        }

        #endregion

        #region Value

        private readonly T _Value;

        /// <summary>
        /// The value.
        /// </summary>
        public T Value
        {
            get
            {
                return _Value;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Timestamped(Value)

        /// <summary>
        /// Create a new timestamped value.
        /// </summary>
        /// <param name="Value">The value.</param>
        public Timestamped(T Value)
            : this(Value, DateTime.Now)
        { }

        #endregion

        #region Timestamped(Value, Timestamp)

        /// <summary>
        /// Create a new timestamped value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="Timestamp">The timestamp.</param>
        public Timestamped(T Value, DateTime Timestamp)
        {
            _Value      = Value;
            _Timestamp  = Timestamp;
        }

        #endregion

        #endregion


        #region Value -implicit-> Timestamped<Value>

        /// <summary>
        /// Implicit conversatiuon from an non-timestamped value
        /// to a timestamped value.
        /// </summary>
        /// <param name="Value">The value to be timestamped.</param>
        public static implicit operator Timestamped<T>(T Value)
        {
            return new Timestamped<T>(Value);
        }

        #endregion

    }

    #endregion

    #region Timestamped_RW<T>

    /// <summary>
    /// A value with its creation timestamp.
    /// </summary>
    /// <typeparam name="T">The type of the timestamped value.</typeparam>
    public struct Timestamped_RW<T>
    {

        #region Properties

        #region Timestamp

        private DateTime _Timestamp;

        /// <summary>
        /// The timestamp of the value creation.
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
        }

        #endregion

        #region Value

        private readonly T _Value;

        /// <summary>
        /// The value.
        /// </summary>
        public T Value
        {
            get
            {
                return _Value;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Timestamped_RW(Value)

        /// <summary>
        /// Create a new timestamped value.
        /// </summary>
        /// <param name="Value">The value.</param>
        public Timestamped_RW(T Value)
            : this(Value, DateTime.Now)
        { }

        #endregion

        #region Timestamped(Value, Timestamp)

        /// <summary>
        /// Create a new timestamped value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="Timestamp">The timestamp.</param>
        public Timestamped_RW(T Value, DateTime Timestamp)
        {
            _Value      = Value;
            _Timestamp  = Timestamp;
        }

        #endregion

        #endregion


        public void UpdateTimestamp()
        {
            _Timestamp = DateTime.Now;
        }

    }

    #endregion

}
