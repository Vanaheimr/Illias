/*
 * Copyright (c) 2010-2012 Achim 'ahzf' Friedland <code@ahzf.de>
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

#endregion

namespace de.ahzf.Illias.Commons
{

    /// <summary>
    /// A factory which uses reflection to generate a apropriate
    /// implementation of T for you.
    /// </summary>
    public class AutoDiscovery<T> : IEnumerable<T>
        where T : class
    {

        #region Data

        private readonly ConcurrentDictionary<String, Type> _TypeDictionary;

        #endregion

        #region Properties

        #region SearchingFor

        /// <summary>
        /// Returns the Name of the interface T.
        /// </summary>
        public String SearchingFor
        {
            get
            {
                return typeof(T).Name;
            }
        }

        #endregion

        #region RegisteredNames

        /// <summary>
        /// Returns an enumeration of the names of all registered types of T.
        /// </summary>
        public IEnumerable<String> RegisteredNames
        {
            get
            {
                return from _StringTypePair in _TypeDictionary select _StringTypePair.Key;
            }
        }

        #endregion

        #region RegisteredTypes

        /// <summary>
        /// Returns an enumeration of activated instances of all registered types of T.
        /// </summary>
        public IEnumerable<T> RegisteredTypes
        {
            get
            {

                try
                {

                    return from _StringTypePair in _TypeDictionary select (T) Activator.CreateInstance(_StringTypePair.Value);

                }
                catch (Exception e)
                {
                    throw new AutoDiscoveryException("Could not create instance! " + e);
                }

            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Returns the number of registered implementations of the interface T.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                return (UInt64) _TypeDictionary.LongCount();
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AutoDiscovery()

        /// <summary>
        /// Create a new AutoDiscovery instance and start the discovery.
        /// </summary>
        public AutoDiscovery()
            : this(true)
        { }

        #endregion

        #region AutoDiscovery(Autostart, IdentificatorFunc = null)

        /// <summary>
        /// Create a new AutoDiscovery instance. An automatic discovery
        /// can be avoided.
        /// </summary>
        /// <param name="Autostart">Automatically start the reflection process.</param>
        /// <param name="IdentificatorFunc">A transformation delegate to provide an unique identification for every matching class.</param>
        public AutoDiscovery(Boolean Autostart, Func<T, String> IdentificatorFunc = null)
        {

            _TypeDictionary = new ConcurrentDictionary<String, Type>();
            
            if (Autostart)
                FindAndRegister(IdentificatorFunc: IdentificatorFunc);

        }

        #endregion

        #endregion


        #region FindAndRegister(ClearTypeDictionary = true, Paths = null, FileExtensions = null, IdentificatorFunc = null)

        /// <summary>
        /// Searches all matching files at the given paths for classes implementing the interface &lt;T&gt;.
        /// </summary>
        /// <param name="ClearTypeDictionary">Clears the TypeDictionary before adding new implementations.</param>
        /// <param name="Paths">An enumeration of paths to search for implementations.</param>
        /// <param name="FileExtensions">A enumeration of file extensions for filtering.</param>
        /// <param name="IdentificatorFunc">A transformation delegate to provide an unique identification for every matching class.</param>
        public void FindAndRegister(Boolean ClearTypeDictionary = true, IEnumerable<String> Paths = null, IEnumerable<String> FileExtensions = null, Func<T, String> IdentificatorFunc = null)
        {

            #region Get a list of interesting files

            var _ConcurrentBag = new ConcurrentBag<String>();

            if (Paths == null)
                Paths = new List<String> { "." };

            if (FileExtensions == null)
                FileExtensions = new List<String> { ".dll", ".exe" };

            foreach (var _Path in Paths)
            {

                Parallel.ForEach(Directory.GetFiles(_Path), _ActualFile =>
                {

                    var _FileInfo = new FileInfo(_ActualFile);

                    if (FileExtensions.Contains(_FileInfo.Extension))
                        _ConcurrentBag.Add(_FileInfo.FullName);

                });

            }

            if (ClearTypeDictionary)
                _TypeDictionary.Clear();

            #endregion

            #region Scan files of implementations of T

            Parallel.ForEach(_ConcurrentBag, _File =>
            {

                // Seems to be a mono bug!
                if (_File != null)
                {

                    Console.WriteLine(_File);

                    try
                    {

                        if (_File != null)
                            foreach (var _ActualType in Assembly.LoadFrom(_File).GetTypes())
                            {

                                if (!_ActualType.IsAbstract &&
                                     _ActualType.IsPublic   &&
                                     _ActualType.IsVisible)
                                {

                                    var _ActualTypeGetInterfaces = _ActualType.GetInterfaces();

                                    if (_ActualTypeGetInterfaces != null)
                                    {

                                        foreach (var _Interface in _ActualTypeGetInterfaces)
                                        {

                                            if (_Interface == typeof(T))
                                            {

                                                try
                                                {

                                                    var __Id = _ActualType.Name;

                                                    if (IdentificatorFunc != null)
                                                    {
                                                        var _T = Activator.CreateInstance(_ActualType) as T;
                                                        if (_T != null)
                                                            __Id = IdentificatorFunc(_T);
                                                    }

                                                    if (__Id != null && __Id != String.Empty)
                                                        _TypeDictionary.TryAdd(__Id, _ActualType);

                                                }

                                                catch (Exception e)
                                                {
                                                    throw new AutoDiscoveryException("Could not activate or register " + typeof(T).Name + "-instance '" + _ActualType.Name + "'!", e);
                                                }

                                            }

                                        }

                                    }

                                }

                            }

                    }

                    catch (Exception e)
                    {
                        throw new AutoDiscoveryException("Autodiscovering implementations of interface '" + typeof(T).Name + "' within file '" + _File + "' failed!", e);
                    }

                }

            });

            #endregion

        }

        #endregion

        #region Activate(myImplementationID)

        /// <summary>
        /// Activates a new instance of an implementation based on its identification.
        /// </summary>
        /// <param name="myImplementationID">The identification of the implementation to activate.</param>
        /// <returns>An activated class implementing interface T.</returns>
        public T Activate(String myImplementationID)
        {

            lock (this)
            {

                try
                {

                    Type _Type;

                    if (_TypeDictionary.TryGetValue(myImplementationID, out _Type))
                        if (_Type != null)
                            return (T) Activator.CreateInstance(_Type);

                }
                catch (Exception e)
                {
                    throw new AutoDiscoveryException(typeof(T).Name + " implementation '" + myImplementationID + "' could not be activated!", e);
                }

                throw new AutoDiscoveryException(typeof(T).Name + " implementation '" + myImplementationID + "' could not be activated!");

            }

        }

        #endregion

        #region TryActivate(myImplementationID, out myInstance)

        /// <summary>
        /// Tries to activate a new instance of an implementation based on its identification.
        /// </summary>
        /// <param name="myImplementationID">The identification of the implementation to activate.</param>
        /// <param name="myInstance">The activated class implementing interface T.</param>
        /// <returns>true|false</returns>
        public Boolean TryActivate(String myImplementationID, out T myInstance)
        {

            lock (this)
            {

                try
                {

                    Type _Type;

                    if (_TypeDictionary.TryGetValue(myImplementationID, out _Type))
                        if (_Type != null)
                        {
                            myInstance = (T) Activator.CreateInstance(_Type);
                            return true;
                        }


                }
                catch (Exception)
                { }

                myInstance = default(T);

                return false;

            }

        }

        #endregion


        public IEnumerator<T> GetEnumerator()
        {

            var _List = new List<T>();

            foreach (var _Type in _TypeDictionary.Keys)
                _List.Add(Activate(_Type));

            return _List.GetEnumerator();

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            var _List = new List<T>();

            foreach (var _Type in _TypeDictionary.Keys)
                _List.Add(Activate(_Type));

            return _List.GetEnumerator();
        
        }

    }

}
