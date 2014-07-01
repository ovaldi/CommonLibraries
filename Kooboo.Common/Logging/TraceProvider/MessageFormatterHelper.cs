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
    public static class MessageFormatterHelper
    {
        public static IMessageFormatter Match(this IEnumerable<IMessageFormatter> formatters, object o)
        {
            var formatter = formatters.Where(it => it.Accpet(o)).FirstOrDefault();

            if (formatter == null)
            {
                Logger.Warn(string.Format("找不到类型:{0}对应的Formatter.", o.GetType()));
                formatter = new StringMessageFormatter();
            }
            return formatter;
        }
        public static string AppendExceptionMessage(string message, Exception ex)
        {
            if (ex == null)
            {
                return message;
            }
            string template = @"Exception information:
Exception type: {0}
Exception message: {1}
Source: {2}
Stack trace: {3}";
            return message + System.Environment.NewLine + string.Format(template, ex.GetType(), ex.Message, ex.Source, ex.StackTrace);
        }

        public static string AppendLogTime(string message)
        {
            string template = @"Local time:{0}
UTC time:{1}";

            return message + System.Environment.NewLine + string.Format(template, DateTime.Now.ToString(System.Globalization.CultureInfo.InstalledUICulture), DateTime.UtcNow.ToString(System.Globalization.CultureInfo.InstalledUICulture));
        }
    }
}
