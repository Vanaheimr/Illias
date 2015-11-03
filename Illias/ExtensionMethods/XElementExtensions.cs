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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// Extensions to the XElement class.
    /// </summary>
    public static class XElementExtensions
    {

        public static void IfElementIsDefined(this XElement   ParentXElement,
                                              XName           XName,
                                              Action<String>  ValueAction)
        {

            if (ValueAction == null)
                return;

            if (ParentXElement == null)
                return;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return;

            if (_XElement.Value.Trim().IsNotNullOrEmpty())
                ValueAction(_XElement.Value.Trim());

        }

        public static XElement ElementOrFail(this XElement  ParentXElement,
                                             XName          XName,
                                             String         Message)
        {

            if (ParentXElement == null)
                throw new Exception(Message);

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                throw new Exception(Message);

            return _XElement;

        }

        public static IEnumerable<XElement> ElementsOrFail(this XElement  ParentXElement,
                                                           XName          XName,
                                                           String         Message)
        {

            if (ParentXElement == null)
                throw new Exception(Message);

            var _XElements = ParentXElement.Elements(XName);

            if (_XElements == null || !_XElements.Any())
                throw new Exception(Message);

            return _XElements;

        }

        public static IEnumerable<TResult> ElementsOrDefault<TResult>(this XElement          ParentXElement,
                                                                      XName                  XName,
                                                                      Func<String, TResult>  Mapper,
                                                                      IEnumerable<TResult>   DefaultValues = null)
        {

            if (DefaultValues == null)
                DefaultValues = new TResult[0];

            if (ParentXElement  == null ||
                XName           == null ||
                Mapper          == null)
                return DefaultValues;

            var _XElements = ParentXElement.
                                 Elements(XName).
                                 SafeSelect(XMLTag => Mapper(XMLTag.Value)).
                                 ToArray();

            if (_XElements == null || !_XElements.Any())
                return DefaultValues;

            return _XElements;

        }

        public static IEnumerable<String> ElementsOrDefault(this XElement        ParentXElement,
                                                            XName                XName,
                                                            IEnumerable<String>  DefaultValues = null)
        {

            return ElementsOrDefault(ParentXElement,
                                     XName,
                                     Text => Text,
                                     DefaultValues);

        }

        public static T MapElement<T>(this XElement      ParentXElement,
                                      XName              XName,
                                      Func<XElement, T>  Mapper,
                                      T                  Default = default(T))
        {

            if (ParentXElement == null || Mapper == null)
                return Default;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return Default;

            return Mapper(_XElement);

        }

        public static IEnumerable<T> MapElement<T>(this XElement                   ParentXElement,
                                                   XName                           XName,
                                                   Func<XElement, IEnumerable<T>>  Mapper,
                                                   T                               Default = default(T))
        {

            if (ParentXElement == null || Mapper == null)
                return new T[] { Default };

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return new T[] { Default };

            return Mapper(_XElement);

        }

        public static IEnumerable<T> MapElements<T>(this XElement                           ParentXElement,
                                                    XName                                   XName,
                                                    Func<XElement, OnExceptionDelegate, T>  Mapper,
                                                    OnExceptionDelegate                     OnException = null)
        {

            if (ParentXElement == null || Mapper == null)
                return new T[0];

            var _XElements = ParentXElement.Elements(XName);

            if (_XElements == null)
                return new T[0];

            return _XElements.Select(XML => Mapper(XML, OnException)).Where(v => v != null);

        }


        #region ElementValueOrDefault(this ParentXElement, XName, DefaultValue)

        /// <summary>
        /// Return the value of the first (in document order) child element with the
        /// specified System.Xml.Linq.XName or the given default value.
        /// </summary>
        /// <param name="ParentXElement">The XML parent element.</param>
        /// <param name="XName">The System.Xml.Linq.XName to match.</param>
        /// <param name="DefaultValue">A default value.</param>
        public static String ElementValueOrDefault(this XElement  ParentXElement,
                                                   XName          XName,
                                                   String         DefaultValue)
        {

            if (ParentXElement == null)
                return DefaultValue;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return DefaultValue;

            return _XElement.Value;

        }

        #endregion

        #region ElementValue(this ParentXElement, XName, ExceptionMessage = null)

        /// <summary>
        /// Return the value of the first (in document order) child element with the
        /// specified System.Xml.Linq.XName or throw an optional exception.
        /// </summary>
        /// <param name="ParentXElement">The XML parent element.</param>
        /// <param name="XName">The System.Xml.Linq.XName to match.</param>
        /// <param name="ExceptionMessage">An optional exception message.</param>
        public static String ElementValueOrFail(this XElement  ParentXElement,
                                                XName          XName,
                                                String         ExceptionMessage = null)
        {

            if (ParentXElement == null)
                throw new Exception(ExceptionMessage);

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
            {

                if (ExceptionMessage.IsNotNullOrEmpty())
                    throw new Exception(ExceptionMessage);

                return null;

            }

            return _XElement.Value;

        }

        #endregion


        public static void UseValue(this XElement   ParentXElement,
                                    XName           XName,
                                    Action<String>  Action)
        {

            if (ParentXElement == null)
                return;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return;

            if (_XElement.Value == null)
                return;

            var ActionLocal = Action;
            if (ActionLocal != null)
                ActionLocal(_XElement.Value);

        }

        public static T MapValueOrNull<T>(this XElement    ParentXElement,
                                          XName            XName,
                                          Func<String, T>  ValueMapper)
        {

            if (ParentXElement == null)
                throw new ArgumentNullException("ParentXElement", "The given XML element must not be null!");

            if (ValueMapper == null)
                throw new ArgumentNullException("ValueMapper", "The given XML element mapper delegate must not be null!");

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return default(T);

            if (_XElement.Value == null)
                return default(T);

            return ValueMapper(_XElement.Value);

        }

        public static T MapValueOrFail<T>(this XElement    ParentXElement,
                                          XName            XName,
                                          Func<String, T>  ValueMapper,
                                          String           ExceptionMessage = null)
        {

            if (ParentXElement == null)
                throw new ArgumentNullException("ParentXElement", "The given XML element must not be null!");

            if (ValueMapper == null)
                throw new ArgumentNullException("ValueMapper", "The given XML element mapper delegate must not be null!");

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                throw new Exception(ExceptionMessage.IsNotNullOrEmpty() ? ExceptionMessage : "The given XML element must not be null!");

            if (_XElement.Value == null)
                throw new Exception(ExceptionMessage.IsNotNullOrEmpty() ? ExceptionMessage : "The given XML element value must not be null!");

            return ValueMapper(_XElement.Value);

        }

        public static T MapValueOrDefault<T>(this XElement    ParentXElement,
                                             XName            XName,
                                             Func<String, T>  ValueMapper,
                                             T                DefaultValue = default(T))
        {

            if (ParentXElement == null)
                return DefaultValue;

            if (ValueMapper == null)
                return DefaultValue;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return DefaultValue;

            if (_XElement.Value == null)
                return DefaultValue;

            return ValueMapper(_XElement.Value);

        }

        public static IEnumerable<T> MapValuesOrDefault<T>(this XElement    ParentXElement,
                                                           XName            XWrapperName,
                                                           XName            XElementsName,
                                                           Func<String, T>  ValueMapper,
                                                           T                DefaultValue = default(T))
        {

            if (ParentXElement == null)
                return new T[1] { DefaultValue };

            if (ValueMapper == null)
                return new T[1] { DefaultValue };

            var _XElement = ParentXElement.Element(XWrapperName);

            if (_XElement == null)
                return new T[1] { DefaultValue };

            var _XElements = _XElement.Elements(XElementsName);

            if (_XElements == null)
                return new T[1] { DefaultValue };

            var __XElements = _XElements.ToArray();

            if (__XElements.Length == 0)
                return new T[1] { DefaultValue };

            return _XElements.Select(__XElement => ValueMapper(__XElement.Value));

        }

        public static IEnumerable<T> MapValuesOrFail<T>(this XElement    ParentXElement,
                                                        XName            XWrapperName,
                                                        String           ExceptionMessage,
                                                        XName            XElementsName,
                                                        Func<String, T>  ValueMapper,
                                                        T                DefaultValue = default(T))
        {

            if (ParentXElement == null)
                throw new ArgumentNullException("ParentXElement", "The given XML element must not be null!");

            if (ValueMapper == null)
                throw new ArgumentNullException("ValueMapper", "The given XML element mapper delegate must not be null!");

            var _XElement = ParentXElement.Element(XWrapperName);

            if (_XElement == null)
                throw new Exception(ExceptionMessage.IsNotNullOrEmpty() ? ExceptionMessage : "The given XML element must not be null!");

            var _XElements = _XElement.Elements(XElementsName);

            if (_XElements == null)
                throw new Exception(ExceptionMessage.IsNotNullOrEmpty() ? ExceptionMessage : "The given XML elements must not be null!");

            var __XElements = _XElements.ToArray();

            if (__XElements.Length == 0)
                throw new Exception(ExceptionMessage.IsNotNullOrEmpty() ? ExceptionMessage : "The given array of XML elements must not be empty!");

            return _XElements.Select(__XElement => ValueMapper(__XElement.Value));

        }


        #region AttributeValueOrDefault(this ParentXElement, XName, DefaultValue)


        /// <summary>
        /// Return the value of the first (in document order) attribute with the
        /// specified System.Xml.Linq.XName or the given default value.
        /// </summary>
        /// <param name="ParentXElement">The XML parent element.</param>
        /// <param name="XName">The System.Xml.Linq.XName to match.</param>
        /// <param name="DefaultValue">A default value.</param>
        public static String AttributeValueOrDefault(this XElement  ParentXElement,
                                                     XName          XName,
                                                     String         DefaultValue)
        {

            if (ParentXElement == null)
                return DefaultValue;

            var _XElement = ParentXElement.Attribute(XName);

            if (_XElement == null)
                return DefaultValue;

            return _XElement.Value;

        }

        #endregion



        #region ToUTF8Bytes(this XML)

        public static Byte[] ToUTF8Bytes(this XElement XML)
        {

            if (XML == null)
                return new Byte[0];

            return Encoding.UTF8.GetBytes(XML.ToString());

        }

        #endregion

    }

}
