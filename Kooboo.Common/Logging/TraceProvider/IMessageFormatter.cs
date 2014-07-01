#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Logging.TraceProvider
{
    public interface IMessageFormatter
    {
        bool Accpet(object message);
        string Format(object message, Exception ex);
    }
}
