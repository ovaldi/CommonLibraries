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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kooboo.Common.Web.WebLogging
{
    public sealed class WebThreadInformation
    {
        // Fields
        private string _accountName;
        private bool _isImpersonating;
        private string _stackTrace;
        private int _threadId;
        internal const string IsImpersonatingKey = "ASPIMPERSONATING";

        // Methods
        public WebThreadInformation(Exception exception)
        {
            this._threadId = Thread.CurrentThread.ManagedThreadId;
            this._accountName = WindowsIdentity.GetCurrent().Name;
            if (exception != null)
            {
                this._stackTrace = new StackTrace(exception, true).ToString();
            }
            else
            {
                this._stackTrace = string.Empty;
            }
            this._isImpersonating = exception.Data.Contains("ASPIMPERSONATING");

        }
        public void FormatToString(WebEventFormatter formatter)
        {
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_thread_id", this.ThreadID.ToString(CultureInfo.InstalledUICulture)));
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_thread_account_name", this.ThreadAccountName));
            if (this.IsImpersonating)
            {
                formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_is_impersonating"));
            }
            else
            {
                formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_is_not_impersonating"));
            }
            formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_stack_trace", this.StackTrace));
        }

        // Properties
        public bool IsImpersonating { get { return _isImpersonating; } }
        public string StackTrace { get { return _stackTrace; } }
        public string ThreadAccountName { get { return _accountName; } }
        public int ThreadID { get { return _threadId; } }
    }
}
