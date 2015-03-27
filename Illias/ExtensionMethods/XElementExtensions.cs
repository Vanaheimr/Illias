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

#region Usings

using System;
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

        public static String ElementValueOrDefault(this XElement  ParentXElement,
                                                   XName          XName,
                                                   String         Default)
        {

            if (ParentXElement == null)
                return Default;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return Default;

            return _XElement.Value;

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

        public static String ElementValueOrFail(this XElement  ParentXElement,
                                                XName          XName,
                                                String         Message)
        {

            if (ParentXElement == null)
                throw new Exception(Message);

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                throw new Exception(Message);

            return _XElement.Value;

        }

        public static String ElementValueOrNull(this XElement  ParentXElement,
                                                XName          XName)
        {

            if (ParentXElement == null)
                return null;

            var _XElement = ParentXElement.Element(XName);

            if (_XElement == null)
                return null;

            return _XElement.Value;

        }

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
