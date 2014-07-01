#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

using System;
using System.Configuration;
using System.Collections;


namespace Kooboo.Common.Web.Routing
{

    public class RouteConfigElement : ConfigurationElement
    {
        public RouteConfigElement(String newName, String newUrl, String routeHandlerType)
        {
            Name = newName;
            Url = newUrl;
            RouteHandlerType = routeHandlerType;
        }


        public RouteConfigElement()
        {
        }


        public RouteConfigElement(string elementName)
        {
            Name = elementName;
        }


        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }


        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }


        [ConfigurationProperty("routeHandlerType", IsRequired = false)]
        public string RouteHandlerType
        {
            get { return (string)this["routeHandlerType"]; }
            set { this["routeHandlerType"] = value; }
        }

        [ConfigurationProperty("routeType", IsRequired = false)]
        public string RouteType
        {
            get { return (string)this["routeType"]; }
            set { this["routeType"] = value; }
        }

        [ConfigurationProperty("defaults", IsRequired = false)]
        public RouteChildElement Defaults
        {
            get { return (RouteChildElement)this["defaults"]; }
            set { this["defaults"] = value; }
        }


        [ConfigurationProperty("constraints", IsRequired = false)]
        public RouteChildElement Constraints
        {
            get { return (RouteChildElement)this["constraints"]; }
            set { this["constraints"] = value; }
        }


        [ConfigurationProperty("dataTokens", IsRequired = false)]
        public RouteChildElement DataTokens
        {
            get { return (RouteChildElement)this["dataTokens"]; }
            set { this["dataTokens"] = value; }
        }


        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
        }


        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            return base.SerializeElement(writer, serializeCollectionKey);
        }
    }
}

