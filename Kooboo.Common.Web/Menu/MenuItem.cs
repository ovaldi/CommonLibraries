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
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Specialized;

namespace Kooboo.Common.Web.Menu
{
    public class MenuItem
    {
        public MenuItem()
            : this(null)
        { }
        public MenuItem(string name)
        {
            this.Name = name;
            Items = new List<MenuItem>();
            RouteValues = new RouteValueDictionary();
            HtmlAttributes = new RouteValueDictionary();

            Visible = true;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Visible { get; set; }
        public string Area { get; set; }

        /// <summary>
        /// 菜单旁边的小标记，如果一些数量值得tips
        /// </summary>
        public Badge Badge { get; set; }

        private bool localizable = true;
        public virtual bool Localizable { get { return localizable; } set { localizable = value; } }

        public string Tips { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
        public RouteValueDictionary HtmlAttributes { get; set; }

        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }
        public virtual IList<MenuItem> Items { get; set; }

        public NameValueCollection ReadOnlyProperties { get; set; }

        #region Initialize
        public bool Initialized { get; set; }
        private IMenuItemInitializer menuItemInitiaizer = new DefaultMenuItemInitializer();
        public IMenuItemInitializer Initializer
        {
            get
            {
                return menuItemInitiaizer;
            }
            set
            {
                menuItemInitiaizer = value;
            }
        }

        public virtual void Initialize(ControllerContext controllerContext)
        {
            if (!this.Initialized)
            {
                Initializer.Initialize(this, controllerContext);

                if (this.Items != null)
                {
                    foreach (var item in this.Items)
                    {
                        item.Initialize(controllerContext);
                    }
                }
            }
        }
        #endregion
        //#region ICloneable Members

        //public object Clone()
        //{
        //    var menuItem = (MenuItem)this.MemberwiseClone();

        //    menuItem.RouteValues = new RouteValueDictionary(this.RouteValues);
        //    menuItem.HtmlAttributes = new RouteValueDictionary(this.HtmlAttributes);

        //    return menuItem;
        //}

        //#endregion
    }
}
