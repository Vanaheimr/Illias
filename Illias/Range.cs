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

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// A generic range of values.
    /// </summary>
    /// <typeparam name="T">The type of the range values.</typeparam>
    public struct Range<T>
    {

        #region Min

        /// <summary>
        /// The minimal value.
        /// </summary>
        public readonly T Min;

        #endregion

        #region Max

        /// <summary>
        /// The maximum value.
        /// </summary>
        public readonly T Max;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Creates a new range of values.
        /// </summary>
        /// <param name="MinValue">The minimal value.</param>
        /// <param name="MaxValue">The maximum value.</param>
        public Range(T MinValue, T MaxValue)
        {
            Min = MinValue;
            Max = MaxValue;
        }

        #endregion

    }

}
