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


namespace Kooboo.Common.Web.Menu
{
    public class Menu
    {
        public string Name { get; set; }
        public Menu()
            : this("")
        {

        }
        public Menu(string name)
        {
            this.Name = name;
            Items = new List<MenuItem>();
        }
        public IList<MenuItem> Items { get; set; }

        public void Initialize(ControllerContext controllerContext)
        {
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    item.Initialize(controllerContext);
                }
            }
        }
    }
}
