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

[assembly: System.Web.PreApplicationStartMethod(typeof(Kooboo.Common.Web.ASPNETApplication), "Initialize")]

namespace Kooboo.Common.Web
{
    public static class ASPNETApplication
    {
        public static void Start()
        {
            //
            Kooboo.Common.ObjectContainer.EngineContext.CreateEngineInstance(new Kooboo.Common.Web.ObjectContainer.WebAppTypeFinder());
        }
    }
}
