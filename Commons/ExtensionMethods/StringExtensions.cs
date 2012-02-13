/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Illias Commons <http://www.github.com/ahzf/Illias>
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

namespace de.ahzf.Illias.Commons
{

    /// <summary>
    /// Extensions to the String class.
    /// </summary>
    public static class StringExtensions
    {

        #region IsNotNullAndContains(this String, Substring)

        /// <summary>
        /// Returns a value indicating whether the specified Substring
        /// occurs within the given string.
        /// </summary>
        /// <param name="String">A string.</param>
        /// <param name="Substring">A substring to search for.</param>
        /// <returns>True if the value parameter occurs within this string.</returns>
        public static Boolean IsNotNullAndContains(this String String, String Substring)
        {

            if (String != null)
                return String.Contains(Substring);

            return false;

        }

        #endregion

    }

}
