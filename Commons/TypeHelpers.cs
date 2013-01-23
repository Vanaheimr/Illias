﻿/*
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
using System.Reflection;
using System.Runtime.CompilerServices;

#endregion

namespace de.ahzf.Illias.Commons
{

    /// <summary>
    /// TypeHelpers
    /// </summary>
    public class TypeHelpers
    {

		private const TypeAttributes AnonymousTypeAttributes = TypeAttributes.NotPublic;

        /// <summary>
        /// Is the given type an anonymous type?
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
		public static bool IsAnonymousType(Type t)
		{
			return t.GetCustomAttributes(typeof (CompilerGeneratedAttribute), false).Length == 1
			       && t.IsGenericType
			       && t.Name.Contains("AnonymousType")
			       && (t.Name.StartsWith("<>") || t.Name.StartsWith("VB$"))
			       && (t.Attributes & AnonymousTypeAttributes) == AnonymousTypeAttributes;
		}

    }

}
