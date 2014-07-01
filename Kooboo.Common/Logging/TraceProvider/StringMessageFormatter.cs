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

namespace Kooboo.Common.Logging.TraceProvider
{
    [Kooboo.Common.ObjectContainer.Dependency.Dependency(typeof(IMessageFormatter), Key = "StringMessageFormatter")]
    public class StringMessageFormatter : IMessageFormatter
    {
        public bool Accpet(object message)
        {
            return message is string;
        }

        public string Format(object message, Exception ex)
        {
            var s = message == null ? "" : message.ToString();
            s = MessageFormatterHelper.AppendLogTime(s);
            s = MessageFormatterHelper.AppendExceptionMessage(s, ex);
            return s;
        }
    }
}
