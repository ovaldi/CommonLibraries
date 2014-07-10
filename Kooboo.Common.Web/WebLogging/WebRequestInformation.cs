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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kooboo.Common.Web.WebLogging
{
    public sealed class WebRequestInformation
    {
        #region Fields
        // Fields
        private string _accountName;
        private IPrincipal _iprincipal;
        private string _requestPath;
        private string _requestUrl;
        private string _userHostAddress;
        #endregion

        #region Methods
        // Methods
        public WebRequestInformation()
        {
            //InternalSecurityPermissions.ControlPrincipal.Assert();
            HttpContext current = HttpContext.Current;
            HttpRequest request = null;
            if (current != null)
            {
                //bool hideRequestResponse = current.HideRequestResponse;
                //current.HideRequestResponse = false;
                request = current.Request;
                //current.HideRequestResponse = hideRequestResponse;
                this._iprincipal = current.User;
            }
            else
            {
                this._iprincipal = null;
            }
            if (request == null)
            {
                this._requestUrl = string.Empty;
                this._requestPath = string.Empty;
                this._userHostAddress = string.Empty;
            }
            else
            {
                this._requestUrl = request.RawUrl;
                this._requestPath = request.Path;
                this._userHostAddress = request.UserHostAddress;
            }
            this._accountName = WindowsIdentity.GetCurrent().Name;
        }

        public void FormatToString(WebEventFormatter formatter)
        {
            string name;
            string authenticationType;
            bool isAuthenticated;
            if (this.Principal == null)
            {
                name = string.Empty;
                authenticationType = string.Empty;
                isAuthenticated = false;
            }
            else
            {
                IIdentity identity = this.Principal.Identity;
                name = identity.Name;
                isAuthenticated = identity.IsAuthenticated;
                authenticationType = identity.AuthenticationType;
            }
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_request_url", this.RequestUrl));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_request_path", this.RequestPath));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_user_host_address", this.UserHostAddress));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_user", name));
            if (isAuthenticated)
            {
                formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_is_authenticated"));
            }
            else
            {
                formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_is_not_authenticated"));
            }
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_authentication_type", authenticationType));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_thread_account_name", this.ThreadAccountName));
        }

        #endregion

        #region Properties

        // Properties
        public IPrincipal Principal
        {
            get
            {
                return this._iprincipal;
            }
        }

        public string RequestPath
        {
            get
            {
                return this._requestPath;
            }
        }

        public string RequestUrl
        {
            get
            {
                return this._requestUrl;
            }
        }

        public string ThreadAccountName
        {
            get
            {
                return this._accountName;
            }
        }

        public string UserHostAddress
        {
            get
            {
                return this._userHostAddress;
            }
        }
        #endregion
    }
}
