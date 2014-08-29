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

namespace Kooboo.Common.Web.Menu
{
    /// <summary>
    ///  菜单旁边的小标记，如果一些数量值得tips
    /// </summary>
    public class Badge
    {
        public string Text { get; set; }
        public IDictionary<string, object> HtmlAttributes { get; set; }
    }
}
