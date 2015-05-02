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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// An internationalized (I18N) text/string.
    /// </summary>
    public class I18NString : IEquatable<I18NString>,
                              IEnumerable<I18NPair>
    {

        #region Data

        private readonly Dictionary<Languages, String> I18NStrings;

        #endregion

        #region Properties

        #region IsEmpty

        /// <summary>
        /// The I18N text is empty.
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return I18NStrings.Count == 0;
            }
        }

        #endregion

        #region IsNotEmpty

        /// <summary>
        /// The I18N text is not empty.
        /// </summary>
        public Boolean IsNotEmpty
        {
            get
            {
                return I18NStrings.Count > 0;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region (private) I18NString()

        /// <summary>
        /// Create a new internationalized (I18N) string.
        /// </summary>
        private I18NString()
        {
            this.I18NStrings = new Dictionary<Languages, String>();
        }

        #endregion

        #region I18NString(Language, Text)

        /// <summary>
        /// Create a new internationalized (I18N) string
        /// based on the given language and string.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        /// <param name="Text">The internationalized (I18N) text.</param>
        public I18NString(Languages Language, String Text)
            : this()
        {
            I18NStrings.Add(Language, Text);
        }

        #endregion

        #region I18NString(Texts)

        /// <summary>
        /// Create a new internationalized (I18N) string
        /// based on the given language and string pairs.
        /// </summary>
        public I18NString(KeyValuePair<Languages, String>[] Texts)
            : this()
        {

            foreach (var Text in Texts)
                I18NStrings.Add(Text.Key, Text.Value);

        }

        #endregion

        #region I18NString(params I18NPairs)

        /// <summary>
        /// Create a new internationalized (I18N) string
        /// based on the given I18N-pairs.
        /// </summary>
        public I18NString(params I18NPair[] I18NPairs)
            : this()
        {

            foreach (var Text in I18NPairs)
                I18NStrings.Add(Text.Language, Text.Text);

        }

        #endregion

        #endregion


        #region (static) Create(Language, Text)

        /// <summary>
        /// Create a new internationalized (I18N) string
        /// based on the given language and string.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        /// <param name="Text">The internationalized (I18N) text.</param>
        public static I18NString Create(Languages  Language,
                                        String     Text)
        {
            return new I18NString(Language, Text);
        }

        #endregion

        #region Add(Language, Text)

        /// <summary>
        /// Add a new language-text-pair to the given
        /// internationalized (I18N) string.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        /// <param name="Text">The internationalized (I18N) text.</param>
        public I18NString Add(Languages  Language,
                              String     Text)
        {

            if (!I18NStrings.ContainsKey(Language))
                I18NStrings.Add(Language, Text);

            else
                I18NStrings[Language] = Text;

            return this;

        }

        #endregion

        #region Add(I18NPair)

        /// <summary>
        /// Add a new language-text-pair to the given
        /// internationalized (I18N) string.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        /// <param name="Text">The internationalized (I18N) text.</param>
        public I18NString Add(I18NPair I18NPair)
        {

            if (!I18NStrings.ContainsKey(I18NPair.Language))
                I18NStrings.Add(I18NPair.Language, I18NPair.Text);

            else
                I18NStrings[I18NPair.Language] = I18NPair.Text;

            return this;

        }

        #endregion

        #region has(Language)

        /// <summary>
        /// Checks if the given language representation exists.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        public Boolean has(Languages Language)
        {
            return I18NStrings.ContainsKey(Language);
        }

        #endregion

        #region this[Language]

        /// <summary>
        /// Get the text specified by the given language.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        /// <returns>The internationalized (I18N) text or String.Empty</returns>
        public String this[Languages Language]
        {

            get
            {

                String Text;

                if (I18NStrings.TryGetValue(Language, out Text))
                    return Text;

                return String.Empty;

            }

            set
            {
                I18NStrings[Language] = value;
            }

        }

        #endregion

        #region Remove(Language)

        /// <summary>
        /// Remove the given language from the internationalized (I18N) text.
        /// </summary>
        /// <param name="Language">The internationalized (I18N) language.</param>
        public I18NString Remove(Languages Language)
        {

            if (I18NStrings.ContainsKey(Language))
                I18NStrings.Remove(Language);

            return this;

        }

        #endregion

        #region Clear()

        /// <summary>
        /// Remove all internationalized (I18N) texts.
        /// </summary>
        public I18NString Clear()
        {
            I18NStrings.Clear();
            return this;
        }

        #endregion


        public Boolean Is(Languages  Language,
                          String     Value)
        {

            if (!I18NStrings.ContainsKey(Language))
                return false;

            return I18NStrings[Language].Equals(Value);

        }

        public Boolean IsNot(Languages  Language,
                             String     Value)
        {

            if (!I18NStrings.ContainsKey(Language))
                return true;

            return !I18NStrings[Language].Equals(Value);

        }


        #region GetEnumerator()

        /// <summary>
        /// Enumerate all internationalized (I18N) texts.
        /// </summary>
        public IEnumerator<I18NPair> GetEnumerator()
        {
            return I18NStrings.Select(kvp => new I18NPair(kvp.Key, kvp.Value)).GetEnumerator();
        }

        /// <summary>
        /// Enumerate all internationalized (I18N) texts.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return I18NStrings.Select(kvp => new I18NPair(kvp.Key, kvp.Value)).GetEnumerator();
        }

        #endregion

        #region Operator overloading

        #region Operator == (I8NString1, I8NString2)

        /// <summary>
        /// Compares two I8N-strings for equality.
        /// </summary>
        /// <param name="I8NString1">A I8N-string.</param>
        /// <param name="I8NString2">Another I8N-string.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (I18NString I8NString1, I18NString I8NString2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(I8NString1, I8NString2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) I8NString1 == null) || ((Object) I8NString2 == null))
                return false;

            return I8NString1.Equals(I8NString2);

        }

        #endregion

        #region Operator != (I8NString1, I8NString2)

        /// <summary>
        /// Compares two I8N-strings for inequality.
        /// </summary>
        /// <param name="I8NString1">A I8N-string.</param>
        /// <param name="I8NString2">Another I8N-string.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (I18NString I8NString1, I18NString I8NString2)
        {
            return !(I8NString1 == I8NString2);
        }

        #endregion

        #endregion

        #region IEquatable<I8NString> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an I8NString.
            var I8NString = Object as I18NString;
            if ((Object) I8NString == null)
                return false;

            return this.Equals(I8NString);

        }

        #endregion

        #region Equals(I8NString)

        /// <summary>
        /// Compares two I8NString for equality.
        /// </summary>
        /// <param name="I8NString">An I8NString to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(I18NString I8NString)
        {

            if ((Object) I8NString == null)
                return false;

            if (I18NStrings.Count != I8NString.Count())
                return false;

            foreach (var i8n in I18NStrings)
            {
                if (i8n.Value != I8NString[i8n.Key])
                    return false;
            }

            return true;

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {

            Int32 ReturnValue = 0;

            foreach (var Value in I18NStrings.
                                      Select(i8n => i8n.Key.GetHashCode() ^ i8n.Value.GetHashCode()))
            {
                ReturnValue ^= Value;
            }

            return ReturnValue;

        }

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Get a string representation of this object.
        /// </summary>
        public override String ToString()
        {

            if (I18NStrings.Count == 0)
                return String.Empty;

            return I18NStrings.
                       Select(i8n => i8n.Key.ToString() + ": " + i8n.Value).
                       AggregateWith("; ");

        }

        #endregion

    }

}
