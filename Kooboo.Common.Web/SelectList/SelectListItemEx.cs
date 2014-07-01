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

namespace Kooboo.Common.Web.SelectList
{
    public class SelectListItemEx : SelectListItem
    {
        public IDictionary<string, object> HtmlAttributes { get; set; }
    }
}
