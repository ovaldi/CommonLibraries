#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer.Dependency;
using Kooboo.Common.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Kooboo.Common.Web.Button
{
    [Dependency(typeof(IButtonPluginExecutor))]
    public class ButtonPluginExecutor : IButtonPluginExecutor
    {
        #region .ctor
        IEnumerable<IButtonPlugin> buttonPlugins;
        IApplyToMatcher applyToMatcher;
        public ButtonPluginExecutor(IEnumerable<IButtonPlugin> buttonPlugins, IApplyToMatcher applyToMatcher)
        {
            this.buttonPlugins = buttonPlugins;
            this.applyToMatcher = applyToMatcher;
        }
        #endregion

        #region MatchTopBarPlugins
        protected virtual IEnumerable<IButtonPlugin> MatchButtonPlugins(RouteData route)
        {
            return this.applyToMatcher.Match(this.buttonPlugins, route);
        }
        #endregion

        #region Execute
        public System.Web.Mvc.ActionResult Execute(System.Web.Mvc.ControllerContext controllerContext, string pluginName)
        {
            var executingPlugin = MatchButtonPlugins(controllerContext.RequestContext.RouteData).Where(it => it.Name.EqualsOrNullEmpty(pluginName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (executingPlugin != null)
            {
                object optionModel = null;
                if (executingPlugin.OptionModelType != null)
                {
                    optionModel = ModelBindHelper.BindModel(executingPlugin.OptionModelType, controllerContext);
                }

                return executingPlugin.Execute(new ButtonPluginContext(controllerContext, optionModel, null));
            }
            else
            {
                throw new Exception("The top bar plugin can not be found.");
            }
        }
        #endregion

        #region LoadTopBarPlugins
        public IEnumerable<IButtonPlugin> LoadButtonPlugins(System.Web.Mvc.ControllerContext controllerContext)
        {
            var matchedPlugins = MatchButtonPlugins(controllerContext.RouteData);

            return matchedPlugins;
        }
        #endregion
    }
}
