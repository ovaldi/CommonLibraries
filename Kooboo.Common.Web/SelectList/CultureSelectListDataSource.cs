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
    public class CultureSelectListDataSource : ISelectListDataSource
    {
        #region ISelectListDataSource Members

        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            var cultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);
            foreach (var c in cultures.OrderBy(c => c.DisplayName))
            {
                yield return new SelectListItem() { Text = c.NativeName, Value = c.Name, Selected = c.Equals(System.Threading.Thread.CurrentThread.CurrentCulture) };
            }
        }

        #endregion
    }
}
