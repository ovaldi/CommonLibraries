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
    public interface IButtonPlugin : IApplyTo
    {
        string Name { get; }

        string DisplayText { get; }

        Type OptionModelType { get; }

        string IconClass { get; }

        string GroupName { get; }

        int Order { get; }

        /// <summary>
        /// CommandTarget==null 是，托管提交动作
        /// 否则，生成一个提交地址
        /// </summary>
        MvcRoute GetMvcRoute(ControllerContext controllerContext);

        /// <summary>
        /// 获得将要生成的Button的HTML 标签
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        IDictionary<string, object> HtmlAttributes(ControllerContext controllerContext);

        /// <summary>
        /// 某条数据是否显示Button
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        bool IsVisibleFor(object dataItem);

        /// <summary>
        /// 执行Topbar plugin
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ActionResult Execute(ButtonPluginContext context);
    }
}
