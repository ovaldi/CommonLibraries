#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Web.Button
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IContainerManager containerManager, Common.ObjectContainer.ITypeFinder typeFinder)
        {
            var buttonGroupTypes = typeFinder.FindClassesOfType<IButtonGroup>();
            containerManager.AddComponent(typeof(IButtonGroup), buttonGroupTypes);

            var buttonPluginTypes = typeFinder.FindClassesOfType<IButtonPlugin>();
            containerManager.AddComponent(typeof(IButtonPlugin), buttonPluginTypes);
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
