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

namespace Kooboo.Common.Web.Button
{
    public interface IButton
    {
        string Name { get; }

        string DisplayText { get; }
               
        string IconClass { get; }

        int Order { get; }    

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
    }
}
