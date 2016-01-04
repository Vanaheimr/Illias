﻿/*
 * Copyright (c) 2010-2016, Achim 'ahzf' Friedland <achim@graphdefined.org>
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

namespace org.GraphDefined.Vanaheimr.Illias.Geometry
{

    /// <summary>
    /// The interface of a PixelValuePair.
    /// </summary>
    /// <typeparam name="T">The internal type of the pixel.</typeparam>
    /// <typeparam name="TValue">The type of the stored values.</typeparam>
    public interface IPixelValuePair<T, TValue> : IPixel<T>,
                                                  IEquatable <IPixelValuePair<T, TValue>>,
                                                  IComparable<IPixelValuePair<T, TValue>>,
                                                  IComparable

        where T : IEquatable<T>, IComparable<T>, IComparable

    {

        /// <summary>
        /// The value stored together with a pixel.
        /// </summary>
        TValue Value { get; set; }

    }

}
