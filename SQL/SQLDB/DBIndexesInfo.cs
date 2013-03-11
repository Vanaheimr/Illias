/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Illias <http://www.github.com/ahzf/Illias>
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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace eu.Vanaheimr.Illias.SQL
{

    public class DBIndexesInfo : IEnumerable<Object>
    {

        //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+
        //| Table       | Non_unique | Key_name | Seq_in_index | Column_name | Collation | Cardinality | Sub_part | Packed | Null | Index_type | Comment |
        //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+
        //| messdaten_1 |          0 | PRIMARY  |            1 | ID          | A         |           7 |     NULL | NULL   |      | BTREE      |         |
        //| messdaten_1 |          1 | INDEX_2  |            1 | Timestamp   | A         |        NULL |     NULL | NULL   |      | BTREE      |         |
        //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+

        public String          Table         { get; set; }
        public Nullable<Int64> Non_unique    { get; set; }
        public String          Key_name      { get; set; }
        public Nullable<Int64> Seq_in_index  { get; set; }
        public String          Column_name   { get; set; }
        public String          Collation     { get; set; }
        public Nullable<Int64> Cardinality   { get; set; }
        public String          Sub_part      { get; set; }
        public String          Packed        { get; set; }
        public String          Null          { get; set; }
        public String          Index_type    { get; set; }
        public String          Comment       { get; set; }

        public IEnumerator<Object> GetEnumerator()
        {
            return new List<Object>() { Table, Non_unique, Key_name, Seq_in_index, Column_name, Collation, Cardinality, Sub_part, Packed, Null, Index_type, Comment }.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Object[12] { Table, Non_unique, Key_name, Seq_in_index, Column_name, Collation, Cardinality, Sub_part, Packed, Null, Index_type, Comment }.GetEnumerator();
        }

    }

}
