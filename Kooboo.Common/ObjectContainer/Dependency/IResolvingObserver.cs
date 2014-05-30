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

namespace Kooboo.Common.ObjectContainer.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResolvingObserver
    {
        int Order { get; }
        object OnResolved(object resolvedObject);
    }
}
