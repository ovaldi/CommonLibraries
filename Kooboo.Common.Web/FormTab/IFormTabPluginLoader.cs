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
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kooboo.Common.Web.FormTab
{
    public interface IFormTabPluginLoader
    {
        IEnumerable<LoadedFormTabPlugin> LoadTabPlugins(ControllerContext controllerContext);

        IEnumerable<LoadedFormTabPlugin> SubmitToTabPlugins(ControllerContext controllerContext);
    }
}
