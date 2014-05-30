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
    /// 用于替代注入容器的InjectAttribute标签，这样可以避免对最终注入窗口的依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
    }
}
