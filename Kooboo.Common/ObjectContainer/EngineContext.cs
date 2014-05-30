﻿#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kooboo.Common.ObjectContainer
{
    /// <summary>
    /// Provides access to the singleton instance of the engine.
    /// </summary>
    public class EngineContext
    {
        #region Initialization Methods
        /// <summary>Initializes a static instance of the factory.</summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate, ITypeFinder typeFinder)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Debug.WriteLine("Constructing engine " + DateTime.Now);
                Singleton<IEngine>.Instance = CreateEngineInstance(typeFinder);
                Debug.WriteLine("Initializing engine " + DateTime.Now);
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.</summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        /// <summary>
        /// Creates a factory instance and adds a http application injecting facility.
        /// </summary>
        /// <returns>A new factory</returns>
        public static IEngine CreateEngineInstance(ITypeFinder typeFinder)
        {
            var engines = typeFinder.FindClassesOfType<IEngine>().ToArray();
            if (engines.Length > 0)
            {
                var defaultEngine = (IEngine)Activator.CreateInstance(engines[0], typeFinder);
                return defaultEngine;
            }
            else
            {
                throw new ApplicationException("Can not found any implementation of IEngine.");
            }
        }

        #endregion

        /// <summary>Gets the singleton engine used to access services.</summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false, new AppDomainTypeFinder());
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
