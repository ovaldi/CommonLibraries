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
using Kooboo.Common.Logging.BuiltinTrace;
using System.Diagnostics;
using System.IO;

namespace Kooboo.Common.Tests.Logging.BuiltinTrace
{
    public class StringMessageFormatterMock : IMessageFormatter
    {
        public bool Accpet(object message)
        {
            return true;
        }

        public string Format(object message, Exception ex)
        {
            var s = message == null ? "" : message.ToString();
            if (ex != null)
            {
                s = s + Environment.NewLine + ex.Message;
            }

            return s;
        }
    }
    [TestClass]
    public class TraceLoggerTests
    {
        [TestMethod]
        public void Test_Info()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            TraceSource traceSource = new TraceSource("Test", SourceLevels.All);
            traceSource.Listeners.Add(new TextWriterTraceListener(sw));
            var traceLogger = new TraceLogger(new IMessageFormatter[] { new StringMessageFormatterMock() }, traceSource);
            var message = "Test_Info";
            traceLogger.Info(message);
            Assert.AreEqual("Test Information: 0 : " + message + "\r\n", sb.ToString());
        }

        [TestMethod]
        public void Test_Warn()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            TraceSource traceSource = new TraceSource("Test", SourceLevels.All);
            traceSource.Listeners.Add(new TextWriterTraceListener(sw));
            var traceLogger = new TraceLogger(new IMessageFormatter[] { new StringMessageFormatterMock() }, traceSource);
            var message = "Test_Warn";
            traceLogger.Warn(message);
            Assert.AreEqual("Test Warning: 1 : " + message + "\r\n", sb.ToString());
        }

        [TestMethod]
        public void Test_Error()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            TraceSource traceSource = new TraceSource("Test", SourceLevels.All);
            traceSource.Listeners.Add(new TextWriterTraceListener(sw));
            var traceLogger = new TraceLogger(new IMessageFormatter[] { new StringMessageFormatterMock() }, traceSource);
            var message = "Test_Error";
            traceLogger.Error(message);
            Assert.AreEqual("Test Error: 2 : " + message + "\r\n", sb.ToString());
        }

        [TestMethod]
        public void Test_Error_Exception()
        {
            //输出到Console
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            TraceSource traceSource = new TraceSource("Test", SourceLevels.All);
            traceSource.Listeners.Add(new TextWriterTraceListener(sw));
            var traceLogger = new TraceLogger(new IMessageFormatter[] { new StringMessageFormatterMock() }, traceSource);
            var message = "Test_Error_Exception";
            Exception ex;
            try
            {
                throw new Exception("Test_Error_Exception");
            }
            catch (Exception e)
            {
                ex = e;
            }
            traceLogger.Error(message, ex);
            Assert.AreEqual("Test Error: 2 : " + message + "\r\n" + ex.Message + "\r\n", sb.ToString());
        }


        [TestMethod]
        public void Test_Fatal()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            TraceSource traceSource = new TraceSource("Test", SourceLevels.All);
            traceSource.Listeners.Add(new TextWriterTraceListener(sw));
            var traceLogger = new TraceLogger(new IMessageFormatter[] { new StringMessageFormatterMock() }, traceSource);
            var message = "Test_Fatal";
            traceLogger.Fatal(message);
            Assert.AreEqual("Test Critical: 3 : " + message + Environment.NewLine, sb.ToString());
        }
    }
}
