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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kooboo.Common.Web
{
    public class AreaHelpers
    {
        // Methods
        public static string GetAreaName(RouteBase route)
        {
            IRouteWithArea area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            Route route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }

        public static string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }
        public static string CombineAreaFilePhysicalPath(string areaName, params string[] paths)
        {
            string basePhysicalPath = Path.Combine(Settings.BaseDirectory, "Areas", areaName);
            return Path.Combine(basePhysicalPath, Path.Combine(paths));
        }
        public static string CombineAreaFileVirtualPath(string areaName, params string[] paths)
        {
            string basePhysicalPath = UrlUtility.Combine("~/", "Areas", areaName);
            return UrlUtility.Combine(basePhysicalPath, UrlUtility.Combine(paths));
        }
    }
}
