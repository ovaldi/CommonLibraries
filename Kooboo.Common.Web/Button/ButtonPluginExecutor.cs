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
using System.Web.Mvc;
using System.Web.Routing;

namespace Kooboo.Common.Web.Button
{
    [Dependency(typeof(IButtonPluginExecutor))]
    public class ButtonPluginExecutor : IButtonPluginExecutor
    {
        #region .ctor
        IEnumerable<IButtonGroup> groups;
        IEnumerable<IButtonPlugin> buttonPlugins;
        IApplyToMatcher applyToMatcher;
        public ButtonPluginExecutor(IEnumerable<IButtonGroup> groups, IEnumerable<IButtonPlugin> buttonPlugins, IApplyToMatcher applyToMatcher)
        {
            this.groups = groups ?? new IButtonGroup[0];
            this.buttonPlugins = buttonPlugins ?? new IButtonPlugin[0];
            this.applyToMatcher = applyToMatcher;
        }
        #endregion

        #region MatchTopBarPlugins
        protected virtual IEnumerable<IButtonPlugin> MatchButtonPlugins(RouteData route, string position)
        {
            return this.applyToMatcher.Match(this.buttonPlugins, route, position);
        }
        protected virtual IEnumerable<IButtonGroup> MatchButtonGroups(RouteData route, string position)
        {
            return this.applyToMatcher.Match(this.groups, route, position);
        }
        #endregion

        #region Execute
        public System.Web.Mvc.ActionResult Execute(System.Web.Mvc.ControllerContext controllerContext, string pluginName, string position = null)
        {
            var executingPlugin = MatchButtonPlugins(controllerContext.RequestContext.RouteData, position).Where(it => it.Name.EqualsOrNullEmpty(pluginName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
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
        public IEnumerable<GroupedButton> LoadButtons(ControllerContext controllerContext, string position = null)
        {
            var matchedButtonPlugins = MatchButtonPlugins(controllerContext.RouteData, position);

            var matchButtonGroups = MatchButtonGroups(controllerContext.RouteData, position);

            var groupedList = new List<GroupedButton>();

            foreach (var group in matchButtonGroups)
            {
                var buttonsInGroup = matchedButtonPlugins
                    .Where(it => !string.IsNullOrEmpty(it.GroupName) && it.GroupName.EqualsOrNullEmpty(group.Name, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(it => it.Order)
                    .ToArray();

                var groupedButton = new GroupedButton(group, buttonsInGroup);

                groupedList.Add(groupedButton);
            }

            groupedList.AddRange(matchedButtonPlugins.Where(it => string.IsNullOrEmpty(it.GroupName)).Select(it => new GroupedButton(it, null)));

            return groupedList.OrderBy(it => it.Group.Order).ToArray();

        }
        #endregion
    }
}
