#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

using System;
using System.Collections;
using System.Configuration;

namespace Kooboo.Common.Web.Routing
{
    public class IgnoreRouteCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new IgnoreRouteElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IgnoreRouteElement)element).Name;
        }
    }
}

