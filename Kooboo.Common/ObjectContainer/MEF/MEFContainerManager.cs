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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer.MEF
{
    public class MEFContainerManager : IContainerManager
    {
        List<ComposablePartCatalog> partCatalogs = new List<ComposablePartCatalog>();
        RegistrationBuilder builder = new RegistrationBuilder();
        List<Type> registerTypes = new List<Type>();
        List<object> resiterObjects = new List<object>();

        CompositionContainer _container;

        CompositionContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (registerTypes)
                    {
                        if (_container == null)
                        {
                            TypeCatalog catalog = new TypeCatalog(registerTypes, builder);

                            _container = new CompositionContainer(catalog);
                        }
                    }

                }
                return _container;
            }
        }

        #region AddResolvingObserver
        public void AddResolvingObserver(IResolvingObserver observer)
        {
            throw new NotSupportedFeaturesException();
        }

        #endregion

        #region AddComponent
        public void AddComponent<TService>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            AddComponent<TService, TService>(key, lifeStyle);
        }

        public void AddComponent(Type service, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            AddComponent(service, service, key, lifeStyle);
        }

        public void AddComponent<TService, TImplementation>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            AddComponent(typeof(TService), typeof(TImplementation), key, lifeStyle);
        }

        public void AddComponent(Type service, Type implementation, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient, params Parameter[] parameters)
        {
            SupportabilityCheck.CheckParameters(parameters);

            if (service.IsGenericTypeDefinition)
            {
                throw new NotSupportedFeaturesException();
            }
            else
            {
                registerTypes.AddRange(new[] { implementation });

                PartBuilder pb = null;

                pb = builder.ForType(implementation);
                if (string.IsNullOrEmpty(key))
                {
                    pb = pb.Export(it => it.AsContractType(service));
                }
                else
                {
                    pb = pb.Export(it => it.AsContractType(service).AsContractName(key));
                }
                pb = pb.SelectConstructor(ctors =>
                    ctors.MinBy(it => it.GetParameters().Length));

                pb = pb.LifeStyle(lifeStyle);
            }
        }
        #endregion

        #region AddComponentInstance
        public void AddComponentInstance<TService>(object instance, string key = "")
        {
            AddComponentInstance(typeof(TService), instance, key);
        }

        public void AddComponentInstance(object instance, string key = "")
        {
            AddComponentInstance(instance.GetType(), instance, key);
        }

        public void AddComponentInstance(Type service, object instance, string key = "")
        {
            throw new NotSupportedFeaturesException();
        }

        #endregion

        #region Resolve

        public T Resolve<T>(string key = "", params Parameter[] parameters) where T : class
        {
            SupportabilityCheck.CheckParameters(parameters);

            return Container.GetExportedValue<T>(key);
        }

        public object Resolve(Type type, string key = "", params Parameter[] parameters)
        {
            SupportabilityCheck.CheckParameters(parameters);

            return Container.GetExportedValue(type, key);
        }
        #endregion

        #region ResolveAll
        public T[] ResolveAll<T>(string key = "")
        {
            return Container.GetExportedValues<T>(key).ToArray();
        }

        public object[] ResolveAll(Type type, string key = "")
        {
            return Container.GetExportedValues(type, key);
        }
        #endregion

        #region TryResolve
        public T TryResolve<T>(string key = "", params Parameter[] parameters)
        {
            SupportabilityCheck.CheckParameters(parameters);
            return Container.GetExportedValueOrDefault<T>(key);
        }

        public object TryResolve(Type type, string key = "", params Parameter[] parameters)
        {
            SupportabilityCheck.CheckParameters(parameters);
            return Container.GetExportedValueOrDefault(type, key);
        }
        #endregion

        #region ResolveUnregistered
        public T ResolveUnregistered<T>() where T : class
        {
            return ResolveUnregistered(typeof(T)) as T;
        }

        public virtual object ResolveUnregistered(Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var parameterInstances = new List<object>();
                foreach (var parameter in parameters)
                {
                    var service = Resolve(parameter.ParameterType);
                    if (service != null)
                        parameterInstances.Add(service);
                }
                return Activator.CreateInstance(type, parameterInstances.ToArray());


            }
            throw new Exception("No contructor was found that had all the dependencies satisfied.");
        }

        #endregion

        #region InjectProperties
        public void InjectProperties(object instance)
        {
            throw new NotSupportedFeaturesException();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }
        #endregion
    }
}
