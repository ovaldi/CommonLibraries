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

namespace Kooboo.Common.Logging
{
    public interface ILogger
    {
        void Info(object message, Exception ex = null);
        void Warn(object message, Exception ex = null);
        void Error(object message, Exception ex = null);
        void Fatal(object message, Exception ex = null);
    }
}
