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
using System.Web;

namespace Kooboo.Common.Web
{
    public interface IHttpApplicationEvents
    {
        void Init(HttpApplication httpApplication);
        void Application_Start(object sender, EventArgs e);
        void Application_End(object sender, EventArgs e);
        void Application_AuthenticateRequest(object sender, EventArgs e);
        void Application_BeginRequest(object sender, EventArgs e);
        void Application_EndRequest(object sender, EventArgs e);
        void Application_Error(object sender, EventArgs e);
        void Session_Start(object sender, EventArgs e);
        void Session_End(object sender, EventArgs e);
    }
}
