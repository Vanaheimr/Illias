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
using System.Linq;
using System.Collections.Generic;
using System.Collections;

#endregion

namespace de.ahzf.Illias.Commons
{

    /// <summary>
    /// Extensions for byte and byte arrays.
    /// </summary>
    public static class ByteExtensions
    {

        #region Reverse(this ByteArray)

        public static Byte[] Reverse(this Byte[] ByteArray)
        {

            Array.Reverse(ByteArray, 0, ByteArray.Length);

            return ByteArray;

        }

        #endregion

        #region Reverse(this ByteArray, Start, Count)

        public static Byte[] Reverse(this Byte[] ByteArray, UInt32 Start, UInt32 Count)
        {

            Array.Reverse(ByteArray, (Int32) Start, (Int32) Count);

            return ByteArray;

        }

        #endregion


        #region ToInt16s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<Int16> ToInt16s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt16(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt16(Value.Reverse().ToArray(), 0);

        }

        #endregion

        #region ToUInt16s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<UInt16> ToUInt16s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt16(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt16(Value.Reverse().ToArray(), 0);

        }

        #endregion


        #region ToInt32s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<Int32> ToInt32s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt32(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt32(Value.Reverse().ToArray(), 0);

        }

        #endregion

        #region ToUInt32s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<UInt32> ToUInt32s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt32(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt32(Value.Reverse().ToArray(), 0);

        }

        #endregion


        #region ToInt64s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<Int64> ToInt64s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt64(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToInt64(Value.Reverse().ToArray(), 0);

        }

        #endregion

        #region ToUInt64s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<UInt64> ToUInt64s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt64(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToUInt64(Value.Reverse().ToArray(), 0);

        }

        #endregion


        #region ToSingles<T>(this IEnumerable, Endian = true)

        public static IEnumerable<Single> ToSingles(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToSingle(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToSingle(Value.Reverse().ToArray(), 0);

        }

        #endregion

        #region ToDoubles<T>(this IEnumerable, Endian = true)

        public static IEnumerable<Double> ToDoubles(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToDouble(Value.ToArray(), 0);

            else
                foreach (var Value in IEnumerable)
                    yield return BitConverter.ToDouble(Value.Reverse().ToArray(), 0);

        }

        #endregion


        public static DateTime UNIXTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        #region ToDateTime32s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<DateTime> ToDateTime32s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return UNIXTime.AddSeconds(BitConverter.ToInt32(Value.ToArray(), 0));

            else
                foreach (var Value in IEnumerable)
                    yield return UNIXTime.AddSeconds(BitConverter.ToInt32(Value.Reverse().ToArray(), 0));

        }

        #endregion

        #region ToDateTime64s<T>(this IEnumerable, Endian = true)

        public static IEnumerable<DateTime> ToDateTime64s(this IEnumerable<IEnumerable<Byte>> IEnumerable, Boolean Endian = true)
        {

            if (Endian)
                foreach (var Value in IEnumerable)
                    yield return UNIXTime.AddSeconds(BitConverter.ToInt64(Value.ToArray(), 0));

            else
                foreach (var Value in IEnumerable)
                    yield return UNIXTime.AddSeconds(BitConverter.ToInt64(Value.Reverse().ToArray(), 0));

        }

        #endregion

    }

}
