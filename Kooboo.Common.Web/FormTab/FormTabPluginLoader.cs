#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Kooboo.Common.Web.FormTab
{
    public class FormTabPluginLoader : IFormTabPluginLoader
    {
        #region .ctor
        IEnumerable<IFormTabPlugin> tabPlugins;
        IApplyToMatcher applyToMatcher;
        public FormTabPluginLoader(IEnumerable<IFormTabPlugin> tabPlugins, IApplyToMatcher applyToMatcher)
        {
            this.tabPlugins = tabPlugins;
            this.applyToMatcher = applyToMatcher;
        }
        #endregion

        #region LoadTabPlugins
        public IEnumerable<LoadedFormTabPlugin> LoadTabPlugins(System.Web.Mvc.ControllerContext controllerContext)
        {
            var matchedTabs = MatchTabPlugins(controllerContext.RequestContext.RouteData).OrderBy(it => it.Order);

            var loadedPlugins = new List<LoadedFormTabPlugin>();

            foreach (var tab in matchedTabs)
            {
                var tabLoadContext = new FormTabContext(controllerContext, new System.Web.Mvc.ViewDataDictionary(controllerContext.Controller.ViewData));
                tab.LoadData(tabLoadContext);
                loadedPlugins.Add(new LoadedFormTabPlugin(tab, tabLoadContext));
            }
            return loadedPlugins;
        }

        #endregion

        protected virtual IEnumerable<IFormTabPlugin> MatchTabPlugins(RouteData route)
        {
            return this.applyToMatcher.Match(this.tabPlugins, route);
        }

        #region SubmitToTabPlugins
        public IEnumerable<LoadedFormTabPlugin> SubmitToTabPlugins(System.Web.Mvc.ControllerContext controllerContext)
        {
            var matchedTabs = MatchTabPlugins(controllerContext.RequestContext.RouteData).OrderBy(it => it.Order);

            var loadedPlugins = new List<LoadedFormTabPlugin>();

            foreach (var tab in matchedTabs)
            {
                object model = null;
                if (tab.ModelType != null)
                {
                    model = ModelBindHelper.BindModel(tab.ModelType, controllerContext);
                }
                FormTabContext tabContext = new FormTabContext(controllerContext, new System.Web.Mvc.ViewDataDictionary(controllerContext.Controller.ViewData) { Model = model });
                tab.Submit(tabContext);
                loadedPlugins.Add(new LoadedFormTabPlugin(tab, tabContext));
            }
            return loadedPlugins;
        }
        #endregion
    }
}