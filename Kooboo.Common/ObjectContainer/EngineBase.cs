#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer
{
    public abstract class EngineBase : IEngine
    {
        #region Ctor
        public EngineBase(ITypeFinder typeFinder, IContainerManager containerManager)
        {
            if (typeFinder == null)
            {
                throw new ArgumentNullException("typeFinder");
            }
            this.TypeFinder = typeFinder;
            this.ContainerManager = containerManager;

        }
        #endregion

        #region Properties

        public virtual IContainerManager ContainerManager
        {
            get;
            protected set;
        }
        public virtual ITypeFinder TypeFinder { get; protected set; }
        #endregion

        #region Utilities

        private void RunStartupTasks()
        {
            var startUpTaskTypes = this.TypeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        private void InitializeContainer()
        {
            //register attributed dependency
            var attrDependency = new DependencyAttributeRegistrator(this.TypeFinder, this.ContainerManager);
            attrDependency.RegisterServices();

            //
            var drTypes = this.TypeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(this.ContainerManager, this.TypeFinder);

        }

        #endregion

        #region Initialize
        public virtual void Initialize()
        {
            InitializeContainer();
            RunStartupTasks();
        }
        #endregion

        #region Container methods

        #region Resolve
        public T Resolve<T>(params Parameter[] parameters) where T : class
        {
            return ContainerManager.Resolve<T>(null, parameters);
        }

        public T Resolve<T>(string name, params Parameter[] parameters) where T : class
        {
            return ContainerManager.Resolve<T>(name, parameters);
        }

        public object Resolve(Type type, string name, params Parameter[] parameters)
        {
            return ContainerManager.Resolve(type, name, parameters);
        }

        public object Resolve(Type type, params Parameter[] parameters)
        {
            return ContainerManager.Resolve(type, null, parameters);
        }
        #endregion

        #region TryResolve
        public T TryResolve<T>(params Parameter[] parameters) where T : class
        {
            return ContainerManager.TryResolve<T>(null, parameters);
        }

        public T TryResolve<T>(string name, params Parameter[] parameters) where T : class
        {
            return ContainerManager.TryResolve<T>(name, parameters);
        }

        public object TryResolve(Type type, string name, params Parameter[] parameters)
        {
            return ContainerManager.TryResolve(type, name, parameters);
        }

        public object TryResolve(Type type, params Parameter[] parameters)
        {
            return ContainerManager.TryResolve(type, null, parameters);
        }
        #endregion

        #region ResolveAll
        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return ContainerManager.ResolveAll(serviceType);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
        #endregion

        public void InjectProperties(object instance)
        {
            ContainerManager.InjectProperties(instance);
        }

        #endregion

    }
}
