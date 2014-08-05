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
using System.Web.Mvc;
using System.Web.Routing;

namespace Kooboo.Common.Web.Button
{
    public interface IButtonPlugin : IButton, IApplyTo
    {
        Type OptionModelType { get; }

        string GroupName { get; }

        /// <summary>
        /// GetMvcRoute==null 是，托管提交动作
        /// 否则，生成一个提交地址
        /// </summary>
        MvcRoute GetMvcRoute(ControllerContext controllerContext);

        /// <summary>
        /// 执行Topbar plugin
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ActionResult Execute(ButtonPluginContext context);
    }
}
