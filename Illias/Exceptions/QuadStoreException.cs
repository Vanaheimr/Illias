/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

#endregion

namespace de.ahzf.Illias
{

    /// <summary>
    /// The base class for all QuadStoreExceptions.
    /// </summary>
    public class QuadStoreException : Exception
    {

        /// <summary>
        /// A general QuadStore exception occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QuadStoreException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }


    /// <summary>
    /// The base class for all QuadStoreExceptions.
    /// </summary>
    public class QuadStoreException<TSystemId, TQuadId, TTransactionId, TSPO, TContext> : QuadStoreException

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPO           : IEquatable<TSPO>,           IComparable<TSPO>,           IComparable
        where TContext       : IEquatable<TContext>,       IComparable<TContext>,       IComparable

    {

        /// <summary>
        /// A general QuadStore exception occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QuadStoreException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }




    

}
