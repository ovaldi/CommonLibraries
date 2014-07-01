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

namespace Kooboo.Common.Web.SelectList
{
    public class EmptySelectListDataSource : ISelectListDataSource
    {
        #region IDropDownListDataSource Members

        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            return new SelectListItem[] { };
        }

        #endregion
    }
}
