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
using System.Web.Management;

namespace Kooboo.Common.Web.WebLogging
{
    internal static class WebEventFormatterExtensions
    {
        public static void FormatToString(this WebApplicationInformation info, WebEventFormatter formatter)
        {
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_application_domain", info.ApplicationDomain));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_trust_level", info.TrustLevel));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_application_virtual_path", info.ApplicationVirtualPath));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_application_path", info.ApplicationPath));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_machine_name", info.MachineName));
        }

    }
}
