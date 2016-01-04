/*
 * Copyright (c) 2010-2016 Achim 'ahzf' Friedland <achim@graphdefined.org>
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

    /// <summary>
    /// A structure to store a start and end time.
    /// </summary>
    public struct StartEndDateTime
    {

        #region Properties

        #region StartTime

        private readonly DateTime _StartTime;

        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
        }

        #endregion

        #region EndTime

        private readonly DateTime? _EndTime;

        /// <summary>
        /// The end time.
        /// </summary>
        public DateTime? EndTime
        {
            get
            {
                return _EndTime;
            }
        }

        #endregion

        #region Duration

        /// <summary>
        /// The duration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return _EndTime.HasValue ? _EndTime.Value - _StartTime : TimeSpan.MaxValue;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new start and end time structure.
        /// </summary>
        /// <param name="StartTime">The start time.</param>
        /// <param name="EndTime">The end time.</param>
        public StartEndDateTime(DateTime   StartTime,
                                DateTime?  EndTime = null)
        {

            #region Initial checks

            if (EndTime.HasValue && StartTime > EndTime)
                throw new ArgumentException("The Starttime must not be after the Endtime!");

            #endregion

            _StartTime  = StartTime;
            _EndTime    = EndTime;

        }

        #endregion


        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return _StartTime.GetHashCode() * 17 ^ _EndTime.GetHashCode();
            }
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Get a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Concat(_StartTime.ToString(), " -> ", _EndTime.ToString());
        }

        #endregion

    }

}
