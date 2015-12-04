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

    /// <summary>
    /// A structure to store a simple time.
    /// </summary>
    public struct Time
    {

        #region Properties

        #region Hour

        private readonly Byte _Hour;

        /// <summary>
        /// The hour.
        /// </summary>
        public Byte Hour
        {
            get
            {
                return _Hour;
            }
        }

        #endregion

        #region Minute

        private readonly Byte _Minute;

        /// <summary>
        /// The minute.
        /// </summary>
        public Byte Minute
        {
            get
            {
                return _Minute;
            }
        }

        #endregion

        #region Second

        private readonly Byte _Second;

        /// <summary>
        /// The second.
        /// </summary>
        public Byte Second
        {
            get
            {
                return _Second;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a simple time.
        /// </summary>
        /// <param name="Hour">The hour.</param>
        /// <param name="Minute">The minute.</param>
        /// <param name="Second">The second.</param>
        public Time(Byte  Hour,
                    Byte  Minute,
                    Byte  Second)
        {

            #region Initial checks

            if (Hour > 23)
                throw new ArgumentException("The value of the parameter is invalid!", "Hour");

            if (Minute > 59)
                throw new ArgumentException("The value of the parameter is invalid!", "Minute");

            if (Second > 59)
                throw new ArgumentException("The value of the parameter is invalid!", "Second");

            #endregion

            _Hour    = Hour;
            _Minute  = Minute;
            _Second  = Second;

        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static TimeSpan operator -  (Time Time1, Time Time2)
        {

            var Hours    = Time1.Hour   - Time2.Hour;
            var Minutes  = Time1.Minute - Time2.Minute;
            var Seconds  = Time1.Second - Time2.Second;

            return new TimeSpan(Hours   >= 0 ? Hours   : 0,
                                Minutes >= 0 ? Minutes : 0,
                                Seconds >= 0 ? Seconds : 0);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static TimeSpan operator +  (Time Time1, Time Time2)
        {

            var Hours   = Time1.Hour   + Time2.Hour;
            var Minutes = Time1.Minute + Time2.Minute;
            var Seconds = Time1.Second + Time2.Second;

            return new TimeSpan(Time1.Hour   + Time2.Hour,
                                Time1.Minute + Time2.Minute,
                                Time1.Second + Time2.Second);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static Boolean operator >  (Time Time1, Time Time2)
        {

            if (Time1.Hour > Time2.Hour)
                return true;

            if (Time1.Hour < Time2.Hour)
                return false;

            if (Time1.Minute > Time2.Minute)
                return true;

            if (Time1.Minute < Time2.Minute)
                return false;

            if (Time1.Second > Time2.Second)
                return true;

            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static Boolean operator <= (Time Time1, Time Time2)
        {

            return !(Time1 > Time2);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static Boolean operator <  (Time Time1, Time Time2)
        {

            if (Time1.Hour > Time2.Hour)
                return false;

            if (Time1.Hour < Time2.Hour)
                return true;

            if (Time1.Minute > Time2.Minute)
                return false;

            if (Time1.Minute < Time2.Minute)
                return true;

            if (Time1.Second > Time2.Second)
                return false;

            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Time1"></param>
        /// <param name="Time2"></param>
        public static Boolean operator >= (Time Time1, Time Time2)
        {

            return !(Time1 < Time2);

        }


        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return _Hour.GetHashCode() * 32 ^ _Minute.GetHashCode() * 17 ^ _Second.GetHashCode();
            }
        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Get a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Concat(_Hour.ToString(), ":", _Minute.ToString(), ":", _Second.ToString());
        }

        #endregion

    }

}
