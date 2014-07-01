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
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> SetActiveItem(this IEnumerable<SelectListItem> listItems, object value)
        {
            if (value == null)
            {
                return listItems;
            }
            string[] values = null;

            if (value is IEnumerable<object>)
            {
                values = ((IEnumerable<object>)value).Select(it => it.ToString()).ToArray();
            }
            else if (value is Enum)
            {
                values = new[] { ((int)value).ToString() };
            }
            else
            {
                values = new[] { value.ToString() };
            }

            return listItems.Select(it => new SelectListItem() { Text = it.Text, Value = it.Value, Selected = values.Contains(it.Value, StringComparer.CurrentCultureIgnoreCase) });
        }

        public static IEnumerable<SelectListItem> EmptyItem(this IEnumerable<SelectListItem> listItems, string emptyLabel)
        {
            return new[] { new SelectListItem() { Text = emptyLabel, Value = "" } }.Concat(listItems);
        }
    }
}
