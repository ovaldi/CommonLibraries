#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Web.WebLogging
{
    [Kooboo.Common.ObjectContainer.Dependency.Dependency(typeof(ILogger), Order = 1)]
    public class WebLogger : ILogger
    {
        public void Info(object message, Exception ex = null)
        {
            Raise(EventType.Information, message, ex);
        }

        public void Warn(object message, Exception ex = null)
        {
            Raise(EventType.Warning, message, ex);
        }

        public void Error(object message, Exception ex = null)
        {
            Raise(EventType.Error, message, ex);
        }

        public void Fatal(object message, Exception ex = null)
        {
            Raise(EventType.Fatal, message, ex);
        }
        internal static void Raise(EventType eventType, object message, Exception ex)
        {
            var webEvent = new WebRequestEventWrapper(message == null ? "" : message.ToString(), null, eventType, ex);
            webEvent.Raise();
        }
    }
}
