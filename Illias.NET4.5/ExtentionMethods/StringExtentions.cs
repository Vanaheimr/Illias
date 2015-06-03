﻿/*
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
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// Extensions to the String class.
    /// </summary>
    public static class StringExtensions
    {

        #region IsNullOrEmpty(GivenString, Delegate, [CallerMemberName] ParameterName = "")

        /// <summary>
        /// Call the given delegate whether the specified string is null or empty.
        /// </summary>
        /// <param name="GivenString">The string.</param>
        /// <param name="Delegate">A delegate to call whenever the given string is null or empty.</param>
        /// <param name="ParameterName">The parameter name of the given string (CallerMemberName).</param>
        public static void IsNullOrEmpty(this String                GivenString,
                                         Action<String>             Delegate,
                                         [CallerMemberName] String  ParameterName = "")
        {

            var _Delegate = Delegate;

            if (String.IsNullOrEmpty(GivenString) && _Delegate != null)
                _Delegate(GivenString);

        }

        #endregion

        #region IsNotNullOrEmpty(GivenString, Delegate, [CallerMemberName] ParameterName = "")

        /// <summary>
        /// Call the given delegate whether the specified string is not null or empty.
        /// </summary>
        /// <param name="GivenString">The string.</param>
        /// <param name="Delegate">A delegate to call whenever the given string is null or empty.</param>
        /// <param name="ParameterName">The parameter name of the given string (CallerMemberName).</param>
        public static void IsNotNullOrEmpty(this String                GivenString,
                                            Action<String>             Delegate,
                                            [CallerMemberName] String  ParameterName = "")
        {

            var _Delegate = Delegate;

            if (!String.IsNullOrEmpty(GivenString) && _Delegate != null)
                _Delegate(GivenString);

        }

        #endregion

        #region FailIfNullOrEmpty(GivenString, ExceptionMessage = null, [CallerMemberName] ParameterName = "")

        /// <summary>
        /// Throws an ArgumentNullException whenever the given string is null or empty.
        /// </summary>
        /// <param name="GivenString">The string.</param>
        /// <param name="ExceptionMessage">An optional message to be added to the exception.</param>
        /// <param name="ParameterName">The parameter name of the given string (CallerMemberName).</param>
        public static void FailIfNullOrEmpty(this String                GivenString,
                                             String                     ExceptionMessage = null,
                                             [CallerMemberName] String  ParameterName    = "")
        {

            if (String.IsNullOrEmpty(GivenString))
            {

                if (String.IsNullOrEmpty(ExceptionMessage))
                    throw new ArgumentNullException(ParameterName);

                else
                    throw new ArgumentNullException(ParameterName, ExceptionMessage);

            }

        }

        #endregion

        #region FailIfNotNullOrEmpty(GivenString, ExceptionMessage = null, [CallerMemberName] ParameterName = "")

        /// <summary>
        /// Throws an ArgumentNullException whenever the given string is not null or empty.
        /// </summary>
        /// <param name="GivenString">The string.</param>
        /// <param name="ExceptionMessage">An optional message to be added to the exception.</param>
        /// <param name="ParameterName">The parameter name of the given string (CallerMemberName).</param>
        public static void FailIfNotNullOrEmpty(this String                GivenString,
                                                String                     ExceptionMessage = null,
                                                [CallerMemberName] String  ParameterName    = "")
        {

            if (!String.IsNullOrEmpty(GivenString))
            {

                if (String.IsNullOrEmpty(ExceptionMessage))
                    throw new ArgumentNullException(ParameterName, "The given parameter must not be null or empty!");

                else
                    throw new ArgumentNullException(ParameterName, ExceptionMessage);

            }

        }

        #endregion

        #region ToKeyValuePairs(Text)

        /// <summary>
        /// Converts the given enumeration of strings into an enumeration of key-value-pairs.
        /// </summary>
        /// <param name="Text">An enumeration of strings.</param>
        public static IEnumerable<String> AggregateIndentedLines(this IEnumerable<String> Text)
        {

            #region Initial checks

            if (Text == null)
                yield break;

            #endregion

            var Enumerator   = Text.GetEnumerator();
            var CurrentLine  = String.Empty;

            while (Enumerator.MoveNext())
            {

                if (Enumerator.Current.StartsWith(" ") || Enumerator.Current.StartsWith("\t"))
                    CurrentLine = CurrentLine + Enumerator.Current.TrimStart();

                else
                {

                    if (CurrentLine != String.Empty)
                        yield return CurrentLine;

                    CurrentLine = Enumerator.Current;

                }

            }

            yield return CurrentLine;

        }

        #endregion

        #region ToKeyValuePairs(Text, Delimiters)

        /// <summary>
        /// Converts the given enumeration of strings into an enumeration of key-value-pairs.
        /// </summary>
        /// <param name="Text">An enumeration of strings.</param>
        /// <param name="Delimiters">The delimiter(s) between keys and values.</param>
        public static IEnumerable<KeyValuePair<String, String>> ToKeyValuePairs(this IEnumerable<String>  Text,
                                                                                params Char[]             Delimiters)
        {

            #region Initial checks

            if (Text == null)
                yield break;

            if (Delimiters == null || Delimiters.Length == 0)
                throw new ArgumentNullException("The given delimiter must not be null or empty!");

            String[] Tokens;

            #endregion

            foreach (var line in Text)
            {

                Tokens = line.Split(Delimiters, 2);

                if (Tokens.Length == 2)
                    yield return new KeyValuePair<String, String>(Tokens[0].Trim(), Tokens[1].Trim());

            }

        }

        #endregion

    }

}
