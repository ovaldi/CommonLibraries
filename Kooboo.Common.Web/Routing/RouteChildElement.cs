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
using System.Collections.Generic;

namespace Kooboo.Common.Web.Routing
{

    public class RouteChildElement : ConfigurationElement
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();


        public Dictionary<string, string> Attributes
        {
            get { return this.attributes; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            attributes.Add(name, value);
            return true;
        }
    }
}
