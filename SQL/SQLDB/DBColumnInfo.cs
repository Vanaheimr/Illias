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

    public class DBColumnInfo : IEnumerable<String>
    {

        //+-----------+------------------+------+-----+---------+----------------+
        //| Field     | Type             | Null | Key | Default | Extra          |
        //+-----------+------------------+------+-----+---------+----------------+
        //| ID        | int(10) unsigned | NO   | PRI | NULL    | auto_increment |
        //| AI_00     | float            | YES  |     | NULL    |                |
        //| AI_01     | float            | YES  |     | NULL    |                |
        //| AI_02     | float            | YES  |     | NULL    |                |
        //| AI_03     | float            | YES  |     | NULL    |                |
        //| AI_04     | float            | YES  |     | NULL    |                |
        //| AI_05     | float            | YES  |     | NULL    |                |
        //| AI_06     | float            | YES  |     | NULL    |                |
        //| AI_07     | float            | YES  |     | NULL    |                |
        //| AI_08     | float            | YES  |     | NULL    |                |
        //| AI_09     | float            | YES  |     | NULL    |                |
        //| Date_Time | datetime         | NO   |     | NULL    |                |
        //| Timestamp | int(10) unsigned | NO   | MUL | NULL    |                |
        //+-----------+------------------+------+-----+---------+----------------+

        public String Field   { get; set; }
        public String Type    { get; set; }
        public String Null    { get; set; }
        public String Key     { get; set; }
        public String Default { get; set; }
        public String Extra   { get; set; }

        public IEnumerator<String> GetEnumerator()
        {
            return new List<String>(){ Field, Type, Null, Key, Default, Extra }.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new String[6] { Field, Type, Null, Key, Default, Extra }.GetEnumerator();
        }

    }

}
