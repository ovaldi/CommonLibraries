#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kooboo.Common.ObjectContainer.MEF;
using Kooboo.Common.ObjectContainer.Dependency;
using Kooboo.Common.ObjectContainer;

namespace Kooboo.Common.Tests.ObjectContainer.MEF
{
    public interface IFoo
    {

    }
    public class Foo : IFoo
    {

    }
    public class Foo2 : IFoo
    {

    }

    public class Bar
    {
        public Bar(IFoo foo)
        {
            this.Foo = foo;
        }
        public IFoo Foo { get; set; }
    }

    public class Bar2 : Bar
    {
        public Bar2(IFoo foo)
            : base(foo)
        { }
        public Bar2(IFoo foo, string name)
            : base(foo)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
    [TestClass]
    public class ContainerManagerTests
    {
        #region Test_AddComponent
        [TestMethod]
        public void Test_AddComponent()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));
            var foo = container.Resolve<IFoo>();
            Assert.IsTrue(foo is Foo);
        }

        [TestMethod]
        public void Test_AddComponent_LifeStyle_Transient()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo), null, Kooboo.Common.ObjectContainer.Dependency.ComponentLifeStyle.Transient);
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();
            Assert.AreNotEqual(foo1, foo2);

        }

        [TestMethod]
        public void Test_AddComponent_LifeStyle_Singleton()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo), null, Kooboo.Common.ObjectContainer.Dependency.ComponentLifeStyle.Singleton);
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();
            Assert.AreEqual(foo1, foo2);

        }

        [TestMethod]
        public void Test_AddComponent_Register_Different_Keys()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));
            container.AddComponent(typeof(IFoo), typeof(Foo2), "Foo2");
            var foo = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>("Foo2");
            Assert.IsTrue(foo is Foo);
            Assert.IsTrue(foo2 is Foo2);

            var fooes = container.ResolveAll<IFoo>();

            Assert.AreNotEqual(2, fooes.Length);
        }
        #endregion

        #region Test_AddComponentInstance
        [ExpectedException(typeof(NotSupportedFeaturesException))]
        [TestMethod]
        public void Test_AddComponentInstance()
        {
            MEFContainerManager container = new MEFContainerManager();
            var bar = new Bar(new Foo());
            container.AddComponentInstance(bar);
            var resolvedBar = container.Resolve<Bar>();
            Assert.AreEqual(bar, resolvedBar);
        }

        #endregion

        [TestMethod]
        public void Test_Dependency_Inject()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));

            container.AddComponent<Bar>();

            var bar = container.Resolve<Bar>();

            Assert.IsNotNull(bar);

            Assert.IsNotNull(bar.Foo);

        }

        [TestMethod]
        public void Test_ResolveUnregistered_ObjectDependency_Inject()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));

            var foo = container.Resolve<IFoo>();

            var bar = container.ResolveUnregistered<Bar>();

            Assert.IsNotNull(bar);

            Assert.IsNotNull(bar.Foo);
        }

        [TestMethod]
        public void Test_Multiple_Constructors()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));

            container.AddComponent<Bar2>();

            var bar = container.Resolve<Bar2>();

            Assert.IsNotNull(bar);

            Assert.IsNotNull(bar.Foo);

            Assert.IsNull(bar.Name);

        }

        #region Expected exceptions
        [ExpectedException(typeof(System.ComponentModel.Composition.ImportCardinalityMismatchException))]
        [TestMethod]
        public void Test_Resolve_Unregistered()
        {
            MEFContainerManager container = new MEFContainerManager();
            var foo = container.Resolve<IFoo>();
        }

        [ExpectedException(typeof(System.ComponentModel.Composition.ImportCardinalityMismatchException))]
        [TestMethod]
        public void Test_AddComponent_MultipuleRegistion()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));
            container.AddComponent(typeof(IFoo), typeof(Foo2));
            var foo = container.Resolve<IFoo>();
        }
        [ExpectedException(typeof(NotSupportedFeaturesException))]
        [TestMethod]
        public void Test_AddComponent_With_Parameters()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo), null, ComponentLifeStyle.Transient, new Parameter("name", "value"));
        }

        [ExpectedException(typeof(NotSupportedFeaturesException))]
        [TestMethod]
        public void Test_AddComponent_NotSupported_LifeStyle()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo), null, ComponentLifeStyle.InRequestScope);
        }


        [ExpectedException(typeof(System.ComponentModel.Composition.ImportCardinalityMismatchException))]
        [TestMethod]
        public void Test_Resolve_Unregistered_Object_Dependency_Inject()
        {
            MEFContainerManager container = new MEFContainerManager();
            container.AddComponent(typeof(IFoo), typeof(Foo));

            var foo = container.Resolve<IFoo>();

            var bar = container.Resolve<Bar>();

            Assert.IsNotNull(bar);

            Assert.IsNotNull(bar.Foo);
        }
        #endregion
    }
}
