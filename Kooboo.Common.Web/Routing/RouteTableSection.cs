#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

using System;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Collections;


namespace Kooboo.Common.Web.Routing
{
    public class RouteTableSection : ConfigurationSection
    {
        public RouteTableSection()
        {
        }

        [ConfigurationProperty("ignores", IsDefaultCollection = false)]
        public IgnoreRouteCollection Ignores
        {
            get
            {
                IgnoreRouteCollection ignoresCollection =
                (IgnoreRouteCollection)base["ignores"];
                return ignoresCollection;
            }
        }

        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteElementCollection Routes
        {
            get
            {
                RouteElementCollection urlsCollection =
                (RouteElementCollection)base["routes"];
                return urlsCollection;
            }
        }


        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
        }


        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            return base.SerializeSection(parentElement, name, saveMode);
        }


        #region IStandaloneConfigurationSection Members

        public void DeserializeSection(string config)
        {
            this.DeserializeSection(new XmlTextReader(new StringReader(config)));
        }

        #endregion
    }
}

