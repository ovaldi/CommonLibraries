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

namespace Kooboo.Common.ObjectContainer.Dependency
{
    public static class IContainerManagerExtensions
    {
        public static void AddComponent(this IContainerManager containerManager, Type service, IEnumerable<Type> implementations)
        {
            foreach (var item in implementations)
            {
                containerManager.AddComponent(service, item, item.FullName);
            }
        }
    }
}
