/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Illias <http://www.github.com/ahzf/Illias>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;

#endregion

namespace de.ahzf.Illias
{

    public class Program
    {

        public static void Main(String[] Args)
        {

            var QuadStore = UnitTests.SimpleQuadStoreTests.CreateTestStore();

            //var AllOf_Alice1     = QuadStore.AllOf("Alice").ToList();
            //var AllOf_Alice2     = QuadStore.GetQuads(Subject:   "Alice").ToList();

            //var AllBy_Love1      = QuadStore.AllBy("loves").ToList();
            //var AllBy_Love2      = QuadStore.GetQuads(Predicate: "loves").ToList();

            //var AllWith_Alice1   = QuadStore.AllWith("Alice").ToList();
            //var AllWith_Alice2   = QuadStore.GetQuads(Object:    "Alice").ToList();

            //var AllFrom_Context1 = QuadStore.AllFrom("0").ToList();
            //var AllFrom_Context2 = QuadStore.GetQuads(Context:   "0"    ).ToList();

            //var test5 = QuadStore.GetQuads(Subject: "Alice",
            //                               Object:  "Bob").ToList();


            //var test6 = QuadStore.SelectQuads(SubjectSelector: s => String.Compare(s, "Alice") >= 0,
            //                                  ObjectSelector:  o => o.EndsWith("e")).ToList();

            var test7a = QuadStore.Traverse("Alice", "knows", true ).ToList();
            var test7b = QuadStore.Traverse("Alice", "knows", false).ToList();

        }

    }

}
