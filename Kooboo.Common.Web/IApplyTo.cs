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

namespace Kooboo.Common.Web
{
    public interface IApplyTo
    {
        IEnumerable<MvcRoute> ApplyTo { get; }
        /// <summary>
        /// 指定扩展的位置，设置位置名称
        /// 没有指定位置，则放在默认位置，比如Button，就默认放在TopBar
        /// </summary>
        string Position { get; }
    }
}
