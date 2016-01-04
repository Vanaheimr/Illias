/*
 * Copyright (c) 2010-2016 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Illias <http://www.github.com/Vanaheimr/Illias>
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
using System.Diagnostics;

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// Helpers for the normal Debug class.
    /// </summary>
    public static class DebugX
    {

        #region Log(Text)

        /// <summary>
        /// Write the current timestamp and given text to Debug.
        /// </summary>
        /// <param name="Text">The text to be logged.</param>
        public static void Log(String Text)
        {
            Debug.WriteLine("[" + DateTime.Now + "] " + Text);
        }

        #endregion

        #region LogT(Text)

        /// <summary>
        /// Write the current timestamp and given text to Debug.
        /// </summary>
        /// <param name="Text">The text to be logged.</param>
        public static void LogT(String Text)
        {
            Debug.WriteLine("[" + DateTime.Now + ", Thread " + Thread.CurrentThread.ManagedThreadId + "] " + Text);
        }

        #endregion

    }

}
