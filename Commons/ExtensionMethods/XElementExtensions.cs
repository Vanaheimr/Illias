﻿/*
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
using System.Linq;
using System.Xml.Linq;

#endregion

namespace eu.Vanaheimr.Illias.Commons
{

    /// <summary>
    /// Extensions to the XElement class.
    /// </summary>
    public static class XElementExtensions
    {

        public static String ElementOrDefault(this XElement  ParentXElement,
                                              XName          XName,
                                              String         Default)
        {

            var XElement = ParentXElement.Element(XName);

            if (XElement != null)
                return ParentXElement.Element(XName).Value;

            else
                return Default;

        }

    }

}