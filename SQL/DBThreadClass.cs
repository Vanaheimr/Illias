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
using System.Threading;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.Windows.Threading;

using MySql.Data.MySqlClient;

#endregion

namespace de.ahzf.Illias.SQL
{

    /// <summary>
    /// A class to keep the database connection running
    /// within its own thread (mySQL wants it this way).
    /// </summary>
    public class DBThreadClass : IDisposable
    {

        #region Data

        private IDbConnection                           CurrentDBConnection;
        private Thread                                  DBThread;
        private ConcurrentQueue<Action<IDbConnection>>  TaskQueue;
        private Dispatcher                              Dispatcher;
        private Boolean                                 EndDatabaseThread;
        private readonly Object                         LockObject;

        #endregion

        #region Properties

        #region DBAccessString

        /// <summary>
        /// The database access string.
        /// </summary>
        public String DBAccessString { get; set; }

        #endregion

        #region IsDefined

        /// <summary>
        /// Checks is the current database connection may be defined.
        /// </summary>
        public Boolean IsDefined
        {
            get
            {
                return CurrentDBConnection != null;
            }
        }

        #endregion

        #region ThreadId

        /// <summary>
        /// The id of this thread.
        /// </summary>
        public Int32 ThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A class to keep the database connection running
        /// within its own thread (mySQL wants it this way).
        /// </summary>
        /// <param name="Dispatcher">The WPF dispatcher.</param>
        public DBThreadClass(Dispatcher Dispatcher)
        {

            this.Dispatcher  = Dispatcher;
            this.TaskQueue   = new ConcurrentQueue<Action<IDbConnection>>();
            this.LockObject  = new Object();

            DBThread = new Thread(() =>
            {

                Thread.CurrentThread.Name = "Database thread";
                Action<IDbConnection> ToDo;

                while (!EndDatabaseThread)
                {

                    if (TaskQueue.TryDequeue(out ToDo))
                        ToDo(CurrentDBConnection);

                    else
                        lock (LockObject) {
                            Monitor.Wait(LockObject);
                        }

                }

                ToDo = null;

            });

            DBThread.Start();

        }

        #endregion


        #region (private) Connect()

        private IDbConnection Connect()
        {

            if (CurrentDBConnection == null || CurrentDBConnection.State == ConnectionState.Closed)
                if (DBAccessString != null)
                {
                    CurrentDBConnection = new MySqlConnection(DBAccessString);
                    CurrentDBConnection.Open();
                }

            return CurrentDBConnection;

        }

        #endregion

        #region Reset()

        /// <summary>
        /// Reset the current database connection.
        /// </summary>
        public Boolean Reset()
        {

            if (CurrentDBConnection != null)
            {

                // If connection not closed => close it!
                if (CurrentDBConnection.State != ConnectionState.Closed)
                {
                    try
                    {
                        CurrentDBConnection.Close();
                        CurrentDBConnection.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }

                CurrentDBConnection = null;
                DBAccessString      = null;

                return true;

            }

            return false;

        }

        #endregion


        #region AddWork(Work, OnSuccess = null, OnError = null)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Work"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="OnError"></param>
        public void AddWork(Action<IDbConnection> Work, Action<Stopwatch> OnSuccess = null, Action<Exception, Stopwatch> OnError = null)
        {

            TaskQueue.Enqueue((_CurrentDBConnection) =>
            {

                var StopWatch = new Stopwatch();
                StopWatch.Start();

                try
                {

                    if (CurrentDBConnection == null || !(CurrentDBConnection.State == ConnectionState.Open))
                        if (DBAccessString != null)
                            _CurrentDBConnection = Connect();

                    Work(_CurrentDBConnection);

                    if (OnSuccess != null)
                        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                                               new Action<Stopwatch>((StopWatch2) =>
                                               {

                                                   try
                                                   {
                                                       OnSuccess(StopWatch2);
                                                   }
                                                   catch (Exception e)
                                                   {
                                                       if (OnError != null)
                                                           OnError(e, StopWatch2);
                                                   }

                                               }), StopWatch);

                }
                catch (Exception e)
                {

                    CurrentDBConnection = null;

                    if (OnError != null)
                        Dispatcher.BeginInvoke(DispatcherPriority.Send,
                                               new Action<Exception, Stopwatch>((e2, sw2) => OnError(e, sw2)), e, StopWatch);

                }

            });

            lock (LockObject) {
                Monitor.Pulse(LockObject);
            }

        }

        #endregion

        #region AddWork<T>(Work, OnSuccess, OnError = null)

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Work"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="OnError"></param>
        public void AddWork<T>(Func<IDbConnection, T> Work, Action<T, Stopwatch> OnSuccess, Action<Exception, Stopwatch> OnError = null)
        {

            TaskQueue.Enqueue((_CurrentDBConnection) =>
            {

                var StopWatch = new Stopwatch();
                StopWatch.Start();

                try
                {

                    if (CurrentDBConnection == null || !(CurrentDBConnection.State == ConnectionState.Open))
                        if (DBAccessString != null)
                            _CurrentDBConnection = Connect();

                    var Result = Work(_CurrentDBConnection);

                    Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                                           new Action<T, Stopwatch>((Result2, StopWatch2) =>
                                           {

                                               try
                                               {
                                                   OnSuccess(Result2, StopWatch2);
                                               }
                                               catch (Exception e)
                                               {
                                                   if (OnError != null)
                                                       OnError(e, StopWatch2);
                                               }

                                           }), Result, StopWatch);

                }
                catch (Exception e)
                {

                    CurrentDBConnection = null;

                    if (OnError != null)
                        Dispatcher.BeginInvoke(DispatcherPriority.Send,
                                               new Action<Exception, Stopwatch>((e2, sw2) => OnError(e2, sw2)), e, StopWatch);

                }

            });

            lock (LockObject) {
                Monitor.Pulse(LockObject);
            }

        }

        #endregion


        #region Dispose()

        /// <summary>
        /// Dispose this object.
        /// </summary>
        public void Dispose()
        {
            lock (LockObject) {
                Reset();
                EndDatabaseThread = true;
                Monitor.Pulse(LockObject);
            }
        }

        #endregion

    }

}
