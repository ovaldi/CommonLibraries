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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kooboo.Common.Web.SelectList
{
    public class EnumTypeSelectListDataSource : ISelectListDataSource
    {
        public EnumTypeSelectListDataSource(Type enumType)
        {
            EnumType = enumType;
        }
        public Type EnumType { get; private set; }
        #region ISelectListDataSource Members

        public IEnumerable<SelectListItem> GetSelectListItems(RequestContext requestContext, string filter)
        {
            foreach (var item in Enum.GetValues(EnumType))
            {
                yield return new SelectListItem() { Text = GetDescription(item), Value = ((int)item).ToString() };
            }
        }
        private string GetDescription(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        #endregion
    }
}
