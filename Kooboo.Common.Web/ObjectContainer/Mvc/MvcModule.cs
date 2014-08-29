#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer;
using Kooboo.Common.ObjectContainer.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kooboo.Common.Web.ObjectContainer.Mvc
{
    [Dependency(typeof(Kooboo.Common.Web.IHttpApplicationEvents), Key = "MvcModule")]
    public class MvcModule : HttpApplicationEvents
    {
        public override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
            RemoveDefaultAttributeFilterProvider();

            DependencyResolver.SetResolver(new MvcDependencyResolver(EngineContext.Current, DependencyResolver.Current));
            FilterProviders.Providers.Add(new MvcDependencyAttributeFilterProvider(EngineContext.Current));
        }
        private static void RemoveDefaultAttributeFilterProvider()
        {
            var oldFilter = FilterProviders.Providers.SingleOrDefault(f => f is FilterAttributeFilterProvider);
            if (oldFilter != null)
            {
                FilterProviders.Providers.Remove(oldFilter);
            }
        }
    }
}
