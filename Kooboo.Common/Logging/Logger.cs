using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Logging
{
    public static class Logger
    {
        public static ILogger LoggerInstance = (ILogger)TypeActivator.CreateInstance(typeof(ILogger));
        public static void Info(object message, Exception ex = null)
        {
            LoggerInstance.Info(message, ex);
        }
        public static void Warn(object message, Exception ex = null)
        {
            LoggerInstance.Warn(message, ex);
        }
        public static void Error(object message, Exception ex = null)
        {
            LoggerInstance.Error(message, ex);
        }
    }
}
