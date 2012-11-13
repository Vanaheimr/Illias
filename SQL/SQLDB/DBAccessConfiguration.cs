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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Illias.SQL
{

    /// <summary>
    /// A database access configuration.
    /// </summary>
    public class DBAccessConfiguration : IEquatable<DBAccessConfiguration>
    {

        #region Properties

        /// <summary>
        /// A description of this database config.
        /// </summary>
        public String Description  { get; private set; }

        /// <summary>
        /// The driver name for this database config, e.g. mySQL or postgresql.
        /// </summary>
        public String Driver       { get; private set; }

        /// <summary>
        /// The server name/ip address for this database config.
        /// </summary>
        public String Server       { get; private set; }

        /// <summary>
        /// The database name for this database config.
        /// </summary>
        public String Database     { get; private set; }

        /// <summary>
        /// The username for this database config.
        /// </summary>
        public String Username     { get; private set; }

        /// <summary>
        /// The password for this database config.
        /// </summary>
        public String Password     { get; private set; }

        /// <summary>
        /// Data Channels
        /// </summary>
        public IDictionary<String, DataChannel> Channels         { get; private set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new database access configuration.
        /// </summary>
        /// <param name="Description">The description of this database config.</param>
        /// <param name="Server">The server name/ip address for this database config.</param>
        /// <param name="Database">The database name for this database config.</param>
        /// <param name="Table">The table name for this database config.</param>
        /// <param name="Username">The username for this database config.</param>
        /// <param name="Password">The password for this database config.</param>
        /// <param name="InverterMapping"></param>
        public DBAccessConfiguration(String Description,
                                     String Driver,
                                     String Server,
                                     String Database,
                                     String Username,
                                     String Password)
        {
            this.Description      = Description;
            this.Driver           = Driver;
            this.Server           = Server;
            this.Database         = Database;
            this.Username         = Username;
            this.Password         = Password;
            this.Channels         = Channels;
        }

        #endregion


        #region DBAccessString

        /// <summary>
        /// Return a valid database access string.
        /// </summary>
        public String DBAccessString
        {
            get
            {

                return String.Concat("Server=",    Server,
                                     ";Database=", Database,
                                     ";User ID=",  Username,
                                     ";Password=", Password,
                                     ";Pooling=false");


            }
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return Description;
        }

        #endregion


        public Boolean Equals(DBAccessConfiguration other)
        {
            return this.Description == other.Description;
        }

    }

}
