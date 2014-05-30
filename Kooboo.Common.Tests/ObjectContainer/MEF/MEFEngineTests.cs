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
    public interface IAttributeDependencyTest
    {

    }
    [Dependency(typeof(IAttributeDependencyTest))]
    public class AttributeDependencyTest : IAttributeDependencyTest
    {

    }
    [TestClass]
    public class MEFEngineTests
    {
        [TestMethod]
        public void Test_Create_MEFEngine()
        {
            var engine = new MEFEngine();
            Assert.IsTrue(engine.ContainerManager is MEFContainerManager);
        }
        [TestMethod]
        public void Test_EngineContext_Current()
        {
            var o = EngineContext.Current.Resolve<IAttributeDependencyTest>();
            Assert.IsTrue(o is IAttributeDependencyTest);
        }
    }
}
