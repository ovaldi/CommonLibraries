#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

using Kooboo.Common.ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Web.PreApplicationStartMethod(typeof(Kooboo.Common.Web.ASPNETApplicationStart), "Start")]

namespace Kooboo.Common.Web
{
    public static class ASPNETApplicationStart
    {
        public static void Start()
        {
            EngineContext.DefaultTypeFinder = new Kooboo.Common.Web.ObjectContainer.WebAppTypeFinder();
            //
            //Kooboo.Common.ObjectContainer.EngineContext.CreateEngineInstance(new Kooboo.Common.Web.ObjectContainer.WebAppTypeFinder());
        }
    }
}
