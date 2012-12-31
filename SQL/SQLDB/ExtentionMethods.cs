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
using System.Data;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Illias.SQL
{

    public static class DatabaseExtentions
    {

        #region ShowTables(this DbConnection, QueryDelegate = null)

        public static IEnumerable<DBTable> ShowTables(this IDbConnection  DbConnection,
                                                      Action<String>      QueryDelegate = null)
        {

            //+-----------------------+
            //| Tables_in_Datenlogger |
            //+-----------------------+
            //| kanalliste            |
            //| messdaten_1           |
            //+-----------------------+

            return DbConnection.Query<DBTable>("SHOW TABLES",
                                               row => new DBTable()
                                               {
                                                   Name = row.GetString(0)
                                               },
                                               QueryDelegate);

        }

        #endregion

        #region ShowTableStatus(this DbConnection, QueryDelegate = null)

        public static IEnumerable<DBTableStatus> ShowTableStatus(this IDbConnection  DbConnection,
                                                                 Action<String>      QueryDelegate = null)
        {

            //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+
            //| Name        | Engine | Version | Row_format | Rows | Avg_row_length | Data_length | Max_data_length   | Index_length | Data_free | Auto_increment | Create_time         | Update_time         | Check_time | Collation         | Checksum | Create_options   | Comment |
            //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+
            //| kanalliste  | MyISAM |      10 | Dynamic    |    0 |              0 |           0 |   281474976710655 |         1024 |         0 |              1 | 2012-09-28 23:43:47 | 2012-09-28 23:43:47 | NULL       | latin1_swedish_ci |     NULL |                  |         |
            //| messdaten_1 | MyISAM |      10 | Fixed      |    7 |             58 |         406 | 16325548649218047 |         3072 |         0 |             11 | 2012-09-28 23:43:48 | 2012-09-29 00:17:17 | NULL       | latin1_swedish_ci |     NULL | row_format=FIXED |         |
            //+-------------+--------+---------+------------+------+----------------+-------------+-------------------+--------------+-----------+----------------+---------------------+---------------------+------------+-------------------+----------+------------------+---------+

            return DbConnection.Query<DBTableStatus>("SHOW TABLE STATUS",
                                                     row => new DBTableStatus()
                                                     {
                                                         Name            = row[0]  as String,
                                                         Engine          = row[1]  as String,
                                                         Version         = !row.IsDBNull(2)  ? new Nullable<Int64>(row.GetInt64(2))  : null,
                                                         Row_format      = row[3]  as String,
                                                         Rows            = !row.IsDBNull(4)  ? new Nullable<Int64>(row.GetInt64(4))  : null,
                                                         Avg_row_length  = !row.IsDBNull(5)  ? new Nullable<Int64>(row.GetInt64(5))  : null,
                                                         Data_length     = !row.IsDBNull(6)  ? new Nullable<Int64>(row.GetInt64(6))  : null,
                                                         Max_data_length = !row.IsDBNull(7)  ? new Nullable<Int64>(row.GetInt64(7))  : null,
                                                         Index_length    = !row.IsDBNull(8)  ? new Nullable<Int64>(row.GetInt64(8))  : null,
                                                         Data_free       = !row.IsDBNull(9)  ? new Nullable<Int64>(row.GetInt64(9))  : null,
                                                         Auto_increment  = !row.IsDBNull(10) ? new Nullable<Int64>(row.GetInt64(10)) : null,
                                                         Create_time     = !row.IsDBNull(11) ? row.GetDateTime(11) : DateTime.MinValue,
                                                         Update_time     = !row.IsDBNull(12) ? row.GetDateTime(12) : DateTime.MinValue,
                                                         Check_time      = !row.IsDBNull(13) ? row.GetDateTime(13) : DateTime.MinValue,
                                                         Collation       = row[14] as String,
                                                         Checksum        = row[15] as String,
                                                         Create_options  = row[16] as String,
                                                         Comment         = row[17] as String
                                                     },
                                                     QueryDelegate);


        }

        #endregion

        // SHOW ERRORS
        // SHOW WARNINGS


        #region ShowColumns(this DbConnection, TableName, QueryDelegate = null)

        public static IEnumerable<DBColumnInfo> ShowColumns(this IDbConnection  DbConnection,
                                                            String              TableName,
                                                            Action<String>      QueryDelegate = null)
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

            return DbConnection.Query<DBColumnInfo>("SHOW columns IN " + TableName,
                                                    row => new DBColumnInfo() {
                                                        Field    = row[0] as String,
                                                        Type     = row[1] as String,
                                                        Null     = row[2] as String,
                                                        Key      = row[3] as String,
                                                        Default  = row[4] as String,
                                                        Extra    = row[5] as String
                                                    },
                                                    QueryDelegate);

        }

        #endregion

        #region ShowIndexes(this DbConnection, TableName, QueryDelegate = null)

        public static IEnumerable<DBIndexesInfo> ShowIndexes(this IDbConnection  DbConnection,
                                                             String              TableName,
                                                             Action<String>      QueryDelegate = null)
        {

            //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+
            //| Table       | Non_unique | Key_name | Seq_in_index | Column_name | Collation | Cardinality | Sub_part | Packed | Null | Index_type | Comment |
            //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+
            //| messdaten_1 |          0 | PRIMARY  |            1 | ID          | A         |           7 |     NULL | NULL   |      | BTREE      |         |
            //| messdaten_1 |          1 | INDEX_2  |            1 | Timestamp   | A         |        NULL |     NULL | NULL   |      | BTREE      |         |
            //+-------------+------------+----------+--------------+-------------+-----------+-------------+----------+--------+------+------------+---------+

            return DbConnection.Query<DBIndexesInfo>("SHOW INDEXES FROM " + TableName,
                                                     row => new DBIndexesInfo()
                                                     {
                                                         Table         = row[0]  as String,
                                                         Non_unique    = !row.IsDBNull(1)  ? new Nullable<Int64>(row.GetInt64(1))  : null,
                                                         Key_name      = row[2]  as String,
                                                         Seq_in_index  = !row.IsDBNull(3)  ? new Nullable<Int64>(row.GetInt64(3))  : null,
                                                         Column_name   = row[4]  as String,
                                                         Collation     = row[5]  as String,
                                                         Cardinality   = !row.IsDBNull(6)  ? new Nullable<Int64>(row.GetInt64(6))  : null,
                                                         Sub_part      = row[7]  as String,
                                                         Packed        = row[8]  as String,
                                                         Null          = row[9]  as String,
                                                         Index_type    = row[10] as String,
                                                         Comment       = row[11] as String
                                                     },
                                                     QueryDelegate);
            

        }

        #endregion

        #region ShowCreateTable(this DbConnection, TableName, QueryDelegate = null)

        public static Tuple<String, String> ShowCreateTable(this IDbConnection  DbConnection,
                                                            String              TableName,
                                                            Action<String>      QueryDelegate = null)
        {
            return DbConnection.Query("SHOW CREATE TABLE " + TableName, QueryDelegate);
        }

        #endregion


        #region Query(this DbConnection, Query, QueryDelegate = null)

        public static Tuple<String, String> Query(this IDbConnection DbConnection,
                                                  String             Query,
                                                  Action<String>     QueryDelegate = null)
        {

            if (QueryDelegate != null)
                QueryDelegate(Query);

            var DBCommand = DbConnection.CreateCommand();
            DBCommand.CommandText    = Query;
            DBCommand.CommandTimeout = Int32.MaxValue;

            var DBRow = DBCommand.ExecuteReader();
            Tuple<String, String> Value = null;

            if (DBRow.Read())
                Value = new Tuple<String, String>(DBRow[0] as String, DBRow[1] as String);

            DBRow.Close();
            DBCommand.Dispose();

            return Value;

        }

        #endregion

        #region Query<T>(this DbConnection, Query, RowDelegate, QueryDelegate = null)

        public static IEnumerable<T> Query<T>(this IDbConnection    DbConnection,
                                              String                Query,
                                              Func<IDataReader, T>  RowDelegate,
                                              Action<String>        QueryDelegate = null)
        {

            if (QueryDelegate != null)
                QueryDelegate(Query);

            var DBCommand = DbConnection.CreateCommand();
            DBCommand.CommandText    = Query;
            DBCommand.CommandTimeout = Int32.MaxValue;

            var DBRow = DBCommand.ExecuteReader();
            var List  = new List<T>();

            while (DBRow.Read())
                List.Add(RowDelegate(DBRow));

            DBRow.Close();
            DBCommand.Dispose();

            return List;

        }

        #endregion

        #region Query<T>(this DbConnection, Query)

        public static IEnumerable<IDataReader> EnumerateRows(this IDbConnection    DbConnection,
                                                      String                Query)
        {

            var DBCommand = DbConnection.CreateCommand();
            DBCommand.CommandText    = Query;
            DBCommand.CommandTimeout = Int32.MaxValue;

            var DBRow = DBCommand.ExecuteReader();

            while (DBRow.Read())
                yield return DBRow;

            DBRow.Close();
            DBCommand.Dispose();

        }

        #endregion

    }

}
