/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
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

#endregion

namespace org.GraphDefined.Vanaheimr.Illias
{

    /// <summary>
    /// Extension methods for delegates.
    /// </summary>
    public static class DelegateExtensions
    {

        #region FailSafeInvoke(this Action)

        /// <summary>
        /// Run the given delegate without worrying about multi-threading side-effects.
        /// </summary>
        /// <param name="Action">A delegate.</param>
        public static void FailSafeInvoke<T>(this Action Action)
        {

            var ActionCopy = Action;

            if (ActionCopy != null)
                ActionCopy();

        }

        #endregion

        #region FailSafeInvoke<T>(this Action, Argument)

        /// <summary>
        /// Run the given delegate without worrying about multi-threading side-effects.
        /// </summary>
        /// <typeparam name="T">The type of the delegate to run.</typeparam>
        /// <param name="Action">A delegate of type T.</param>
        /// <param name="Argument">The parameter of the delegate.</param>
        public static void FailSafeInvoke<T>(this Action<T> Action, T Argument)
        {

            var ActionCopy = Action;

            if (ActionCopy != null)
                ActionCopy(Argument);

        }

        #endregion

        #region FailSafeInvoke<T>(this Action, Argument1, Argument2)

        /// <summary>
        /// Run the given delegate without worrying about multi-threading side-effects.
        /// </summary>
        /// <typeparam name="T">The type of the delegate to run.</typeparam>
        /// <param name="Action">A delegate of type T.</param>
        /// <param name="Argument1">The first parameter of the delegate.</param>
        /// <param name="Argument2">The second parameter of the delegate.</param>
        public static void FailSafeInvoke<T1, T2>(this Action<T1, T2> Action, T1 Argument1, T2 Argument2)
        {

            var ActionCopy = Action;

            if (ActionCopy != null)
                ActionCopy(Argument1, Argument2);

        }

        #endregion

    }

}
