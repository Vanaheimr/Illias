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

namespace de.ahzf.Illias.SQL
{

    public class DBTableStatus : IEnumerable<Object>
    {

        //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+
        //| Name        | Engine | Version | Row_format | Rows | Avg_row_length | Data_length | Max_data_length   | Index_length | Data_free | Auto_increment | Create_time         | Update_time         | Check_time | Collation         | Checksum | Create_options   | Comment |
        //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+
        //| kanalliste  | MyISAM |      10 | Dynamic    |    0 |              0 |           0 |   281474976710655 |         1024 |         0 |              1 | 2012-09-28 23:43:47 | 2012-09-28 23:43:47 | NULL       | latin1_swedish_ci |     NULL |                  |         |
        //| messdaten_1 | MyISAM |      10 | Fixed      |    7 |             58 |         406 | 16325548649218047 |         3072 |         0 |             11 | 2012-09-28 23:43:48 | 2012-09-29 00:17:17 | NULL       | latin1_swedish_ci |     NULL | row_format=FIXED |         |
        //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+

        public String          Name             { get; set; }
        public String          Engine           { get; set; }
        public Nullable<Int64> Version          { get; set; }
        public String          Row_format       { get; set; }
        public Nullable<Int64> Rows             { get; set; }
        public Nullable<Int64> Avg_row_length   { get; set; }
        public Nullable<Int64> Data_length      { get; set; }
        public Nullable<Int64> Max_data_length  { get; set; }
        public Nullable<Int64> Index_length     { get; set; }
        public Nullable<Int64> Data_free        { get; set; }
        public Nullable<Int64> Auto_increment   { get; set; }
        public DateTime        Create_time      { get; set; }
        public DateTime        Update_time      { get; set; }
        public DateTime        Check_time       { get; set; }
        public String          Collation        { get; set; }
        public String          Checksum         { get; set; }
        public String          Create_options   { get; set; }
        public String          Comment          { get; set; }


        public IEnumerator<Object> GetEnumerator()
        {
            return new List<Object>() { Name, Engine, Version, Row_format, Rows, Avg_row_length, Data_length, Max_data_length, Index_length, Data_free, Auto_increment, Create_time, Update_time, Check_time, Collation, Checksum, Create_options, Comment }.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Object[18] { Name, Engine, Version, Row_format, Rows, Avg_row_length, Data_length, Max_data_length, Index_length, Data_free, Auto_increment, Create_time, Update_time, Check_time, Collation, Checksum, Create_options, Comment }.GetEnumerator();
        }

    }

}
