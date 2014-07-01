#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Logging.TraceProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Tests.Logging.TraceProvider
{
    [TestClass]
    public class StringMessageFormatterTests
    {
        [TestMethod]
        public void Test_Accept()
        {
            var stringMessageFormatter = new StringMessageFormatter();

            Assert.IsTrue(stringMessageFormatter.Accpet(""));
        }
        [TestMethod]
        public void Test_Format()
        {
            var stringMessageFormatter = new StringMessageFormatter();

            string expected = string.Format(@"Test_Format
Local time:{0}
UTC time:{1}", DateTime.Now.ToString(System.Globalization.CultureInfo.InstalledUICulture), DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InstalledUICulture));

            Assert.AreEqual(expected, stringMessageFormatter.Format("Test_Format", null));
        }

        [TestMethod]
        public void Test_Format_Exception()
        {
            var stringMessageFormatter = new StringMessageFormatter();
            Exception ex = new Exception("Exception message");
            string expected = string.Format(@"Test_Format_Exception
Local time:{0}
UTC time:{1}
Exception information:
Exception type: {2}
Exception message: {3}
Source: 
Stack trace: ", DateTime.Now.ToString(System.Globalization.CultureInfo.InstalledUICulture), DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InstalledUICulture), ex.GetType(), ex.Message);

            Assert.AreEqual(expected, stringMessageFormatter.Format("Test_Format_Exception", ex));
        }
    }
}
