#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Logging;
using Kooboo.Common.Logging.BuiltinTrace;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Tests.Logging
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Test_Get_Default_Logger()
        {
            Assert.IsNotNull(Logger.LoggerInstance);
            Assert.IsTrue(Logger.LoggerInstance is TraceLogger);
        }
    }
}
