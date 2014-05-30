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
using System.Diagnostics;
namespace Kooboo.Common.Logging.BuiltinTrace
{
    [Kooboo.Common.ObjectContainer.Dependency.Dependency(typeof(ILogger), Order = -1)]
    public class TraceLogger : ILogger
    {
        IMessageFormatter[] _formatters;
        TraceSource _traceSource;
        #region .ctor
        public TraceLogger(IMessageFormatter[] formatters)
            : this(formatters, new TraceSource("Default", SourceLevels.All))
        {

        }
        public TraceLogger(IMessageFormatter[] formatters, TraceSource traceSource)
        {
            _formatters = formatters;
            this._traceSource = traceSource;
        }
        #endregion

        public TraceSource TraceSource
        {
            get
            {
                return _traceSource;
            }
        }

        public void Info(object message, Exception ex = null)
        {
            var formatter = _formatters.Match(message);
            if (formatter != null)
            {
                TraceSource.TraceEvent(TraceEventType.Information, 0, formatter.Format(message, ex));
            }
        }

        public void Warn(object message, Exception ex = null)
        {
            var formatter = _formatters.Match(message);
            if (formatter != null)
            {
                TraceSource.TraceEvent(TraceEventType.Warning, 1, formatter.Format(message, ex));
            }
        }

        public void Error(object message, Exception ex = null)
        {
            var formatter = _formatters.Match(message);
            if (formatter != null)
            {
                TraceSource.TraceEvent(TraceEventType.Error, 2, formatter.Format(message, ex));
            }
        }

        public void Fatal(object message, Exception ex = null)
        {
            var formatter = _formatters.Match(message);
            if (formatter != null)
            {
                TraceSource.TraceEvent(TraceEventType.Critical, 3, formatter.Format(message, ex));
            }
        }
    }
}
