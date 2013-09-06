/*
 * Copyright (c) 2010-2013 Achim 'ahzf' Friedland <achim@graph-database.org>
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
    /// Extensions to the IList interface.
    /// </summary>
    public static class IListExtensions
    {

        #region Add2(this List)

        /// <summary>
        /// Another way to add a value to a list.
        /// </summary>
        /// <param name="List">A list of values.</param>
        /// <param name="Value">The value to add to the list.</param>
        /// <returns>The changed list.</returns>
        public static IList<T> Add2<T>(this IList<T> List, T Value)
        {
            List.Add(Value);
            return List;
        }

        #endregion

    }

}
