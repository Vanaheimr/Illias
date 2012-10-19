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
    /// A inverter config.
    /// </summary>
    public class DataChannel
    {

        #region Properties

        /// <summary>
        /// The id of this channel.
        /// </summary>
        public String ChannelName { get; private set; }

        /// <summary>
        /// The label of this channel.
        /// </summary>
        public String InterfaceName { get; private set; }

        /// <summary>
        /// The id of this channel.
        /// </summary>
        public String Unit { get; private set; }

        /// <summary>
        /// The label of this channel.
        /// </summary>
        public String DBColumn { get; private set; }

        /// <summary>
        /// The (mutable) aggregation function (MIN, MAX, AVG, COUNT, SUM, ...).
        /// </summary>
        public String AggregationFunction { get; set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new database config.
        /// </summary>
        /// <param name="Id">The id of this channel.</param>
        /// <param name="Label">The label of this channel.</param>
        public DataChannel(String ChannelName, String InterfaceName, String Unit, String DBColumn, String AggregationFunction = "AVG")
        {
            this.ChannelName         = ChannelName;
            this.InterfaceName       = InterfaceName;
            this.Unit                = Unit;
            this.DBColumn            = DBColumn;
            this.AggregationFunction = AggregationFunction;
        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Concat(InterfaceName, " in ", Unit);
        }

        #endregion

    }

}
