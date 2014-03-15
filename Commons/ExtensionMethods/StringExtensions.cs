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
using System.Text;

#endregion

namespace eu.Vanaheimr.Illias.Commons
{

    /// <summary>
    /// Extensions to the String class.
    /// </summary>
    public static class StringExtensions
    {

        #region IsNullOrEmpty(myString)

        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="myString">The string.</param>
        public static Boolean IsNullOrEmpty(this String myString)
        {
            return String.IsNullOrEmpty(myString);
        }

        #endregion

        #region IsNotNullOrEmpty(myString)

        /// <summary>
        /// Indicates whether the specified string is not null or an System.String.Empty string.
        /// </summary>
        /// <param name="myString">The string.</param>
        public static Boolean IsNotNullOrEmpty(this String myString)
        {
            return !String.IsNullOrEmpty(myString);
        }

        #endregion

        #region ToBase64(myString)

        public static String ToBase64(this String myString)
        {

            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(myString));
            }

            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }

        }

        #endregion

        #region FromBase64(myBase64String)

        public static String FromBase64(this String myBase64String)
        {

            try
            {

                var _UTF8Decoder  = new UTF8Encoding().GetDecoder();
                var _Bytes        = Convert.FromBase64String(myBase64String);
                var _DecodedChars = new Char[_UTF8Decoder.GetCharCount(_Bytes, 0, _Bytes.Length)];
                _UTF8Decoder.GetChars(_Bytes, 0, _Bytes.Length, _DecodedChars, 0);

                return new String(_DecodedChars);

            }

            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }

        }

        #endregion

        #region EscapeForXMLandHTML(myString)

        public static String EscapeForXMLandHTML(this String myString)
        {

            if (myString == null)
                throw new ArgumentNullException("myString must not be null!");

            myString = myString.Replace("<", "&lt;");
            myString = myString.Replace(">", "&gt;");
            myString = myString.Replace("&", "&amp;");

            return myString;

        }

        #endregion

        #region ToUTF8String(this myByteArray, NumberOfBytes = -1, ThrowException = true)

        public static String ToUTF8String(this Byte[] ArrayOfBytes, Int32 NumberOfBytes = -1, Boolean ThrowException = true)
        {

            if (ArrayOfBytes == null)
            {
                if (ThrowException)
                    throw new ArgumentNullException("ArrayOfBytes must not be null!");
                else
                    return String.Empty;
            }

            if (ArrayOfBytes.Length == 0)
                return String.Empty;

            NumberOfBytes = (NumberOfBytes > -1) ? NumberOfBytes : ArrayOfBytes.Length;

            return Encoding.UTF8.GetString(ArrayOfBytes, 0, NumberOfBytes);

        }

        #endregion

        #region ToUTF8Bytes(this myString)

        public static Byte[] ToUTF8Bytes(this String myString)
        {

            if (myString == null)
                throw new ArgumentNullException("myString must not be null!");

            return Encoding.UTF8.GetBytes(myString);

        }

        #endregion

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

        #region DoubleNewLine

        /// <summary>
        /// NewLine but twice.
        /// </summary>
        public static String DoubleNewLine
        {
            get
            {
                return Environment.NewLine + Environment.NewLine;
            }
        }

        #endregion

        #region RemoveQuotes(this String)

        /// <summary>
        /// Removes leading and/or tailing (double) quotes.
        /// </summary>
        /// <param name="String">The string to check.</param>
        public static String RemoveQuotes(this String String)
        {

            var Length       = String.Length;
            var LeadingQuote = String.StartsWith("\"") || String.StartsWith("\'");
            var TailingQuote = String.EndsWith("\"")   || String.EndsWith("\'");

            if (!LeadingQuote && !TailingQuote)
                return String;

            if (LeadingQuote && TailingQuote && Length > 2)
                return String.Substring(1, Length - 1);

            if (LeadingQuote && Length > 1)
                return String.Substring(1, Length);

            if (TailingQuote && Length > 1)
                return String.Substring(0, Length - 1);

            return String.Empty;

        }

        #endregion

        #region RemoveAllBefore(this String, Substring)

        /// <summary>
        /// Removes everything from the string before the given substring.
        /// </summary>
        /// <param name="String">A string.</param>
        /// <param name="Substring">A substring.</param>
        public static String RemoveAllBefore(this String String, String Substring)
        {
            return String.Remove(0, String.IndexOf(Substring) + Substring.Length);
        }

        #endregion

        #region RemoveAllAfter(this String, Substring)

        /// <summary>
        /// Removes everything from the string after the given substring.
        /// </summary>
        /// <param name="String">A string.</param>
        /// <param name="Substring">A substring.</param>
        public static String RemoveAllAfter(this String String, String Substring)
        {
            return String.Remove(String.IndexOf(Substring));
        }

        #endregion

        #region LastIndexOfOrMax(this Text, Pattern)

        public static Int32 LastIndexOfOrMax(this String Text, String Pattern)
        {

            var Index = Text.LastIndexOf(Pattern);

            if (Index < 0)
                return Text.Length;

            else
                return Index;

        }

        #endregion

        #region SubstringMax(this Text, Length)

        public static String SubstringMax(this String Text, Int32 Length)
        {
            try
            {
                return Text.Substring(0, Math.Min(Text.Length, Length));
            }
            catch (Exception e)
            {
                return "";
            }
        }

        #endregion

        #region SubTokens(this Text, Length)

        public static IEnumerable<String> SubTokens(this String Text, UInt16 Length)
        {

            var TextCharacterEnumerator  = Text.ToCharArray().GetEnumerator();
            var Characters               = new List<Char>();

            while (TextCharacterEnumerator.MoveNext())
            {

                Characters.Add((Char) TextCharacterEnumerator.Current);

                if (Characters.Count == Length)
                {
                    yield return new String(Characters.ToArray());
                    Characters.Clear();
                }

            }

        }

        #endregion

    }

}
