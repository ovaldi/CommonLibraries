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
using System.Configuration;

namespace Kooboo.Common.Web.Menu.Configuration
{
    public class MenuSection : ConfigurationSectionEx
    {
        static string SectionName = "menu";
        [ConfigurationProperty("items", IsDefaultCollection = false)]
        public MenuItemElementCollection Items
        {
            get
            {
                MenuItemElementCollection itemsCollection =
                (MenuItemElementCollection)base["items"];
                return itemsCollection;
            }
        }

        public static MenuSection GetSection()
        {
            return (MenuSection)ConfigurationManager.GetSection(SectionName);
        }

        public static MenuSection GetSection(string configFile)
        {
            MenuSection section = new MenuSection();
            section.GetSection(configFile, SectionName);
            return section;
        }
    }
}
