/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Illias Commons <http://www.github.com/ahzf/Illias>
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
using System.Threading;

#endregion

namespace de.ahzf.Illias.Commons.Votes
{

    /// <summary>
    /// A vote is a simple way to ask multiple event subscribers
    /// about their opinion and to evaluate the results.
    /// </summary>
    /// <typeparam name="TResult">The type of the voting result.</typeparam>
    public abstract class AVote<TResult> : IVote<TResult>
    {

        #region Data

        /// <summary>
        /// The current number of positive votes.
        /// </summary>
        protected Int32 _NumberOfPositiveVotes;

        /// <summary>
        /// The current total number of votes.
        /// </summary>
        protected Int32 _TotalNumberOfVotes;

        /// <summary>
        /// A delegate for evaluating a vote based on the
        /// overall number of votes and a shared integer.
        /// </summary>
        protected VoteEvaluator<TResult> VoteEvaluator;

        #endregion

        #region Properties

        #region NumberOfVotes

        /// <summary>
        /// The current number of votes.
        /// </summary>
        public UInt32 NumberOfVotes
        {
            get
            {
                return (UInt32) _TotalNumberOfVotes;
            }
        }

        #endregion

        #region Result

        /// <summary>
        /// The result of the voting.
        /// </summary>
        public TResult Result
        {
            get
            {
                lock (this)
                {
                    return VoteEvaluator(_TotalNumberOfVotes, _NumberOfPositiveVotes);
                }
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AVote(VoteEvaluator)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// about their opinion and to evaluate the results.
        /// </summary>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on the overall number of votes and a shared integer.</param>
        public AVote(VoteEvaluator<TResult> VoteEvaluator)
        {

            #region Initial Checks

            if (VoteEvaluator == null)
                throw new ArgumentNullException("VoteEvaluator", "The given VoteEvaluator delegate must not be null!");

            #endregion

            this._NumberOfPositiveVotes          = 0;
            this._TotalNumberOfVotes = 0;
            this.VoteEvaluator  = VoteEvaluator;

        }

        #endregion

        #region AVote(InitialVote, VoteEvaluator)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// about their opinion and to evaluate the results.
        /// </summary>
        /// <param name="InitialVote">An initial vote.</param>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on the overall number of votes and a shared integer.</param>
        public AVote(Int32 InitialVote, VoteEvaluator<TResult> VoteEvaluator)
        {

            #region Initial Checks

            if (VoteEvaluator == null)
                throw new ArgumentNullException("VoteEvaluator", "The given VoteEvaluator delegate must not be null!");

            #endregion

            this._NumberOfPositiveVotes          = InitialVote;
            this._TotalNumberOfVotes = 0;
            this.VoteEvaluator  = VoteEvaluator;

        }

        #endregion

        #endregion


        #region Yes()

        /// <summary>
        /// Vote 'yes' or 'ok' or 'allow'.
        /// </summary>
        public void Yes()
        {
            Interlocked.Increment(ref _NumberOfPositiveVotes);
            Interlocked.Increment(ref _TotalNumberOfVotes);
        }

        #endregion

        #region Ok()

        /// <summary>
        /// Vote 'yes' or 'ok' or 'allow'.
        /// </summary>
        public void Ok()
        {
            Interlocked.Increment(ref _NumberOfPositiveVotes);
            Interlocked.Increment(ref _TotalNumberOfVotes);
        }

        #endregion

        #region Allow()

        /// <summary>
        /// Vote 'yes' or 'ok' or 'allow'.
        /// </summary>
        public void Allow()
        {
            Interlocked.Increment(ref _NumberOfPositiveVotes);
            Interlocked.Increment(ref _TotalNumberOfVotes);
        }

        #endregion


        #region No()

        /// <summary>
        /// Vote 'no' or 'deny'.
        /// </summary>
        public void No()
        {
            Interlocked.Increment(ref _TotalNumberOfVotes);
        }

        #endregion

        #region Deny()

        /// <summary>
        /// Vote 'no' or 'deny'.
        /// </summary>
        public void Deny()
        {
            Interlocked.Increment(ref _TotalNumberOfVotes);
        }

        #endregion

    }

}
