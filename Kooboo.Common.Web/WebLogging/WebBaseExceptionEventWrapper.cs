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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Web.WebLogging
{
    public class WebBaseExceptionEventWrapper : WebBaseEventWrapper
    {
        public WebBaseExceptionEventWrapper(string message, object eventSource, EventType eventType, Exception e)
            : base(message, eventSource, eventType)
        {
            this.Init(e);
        }
        private void Init(Exception e)
        {
            this._exception = e;
        }
        private Exception _exception;
        public Exception ErrorException
        {
            get
            {
                return this._exception;
            }
        }

        internal override void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
        {
            base.GenerateFieldsForMarshal(fields);
            if (this._exception != null)
            {
                fields.Add(new WebEventFieldData("ExceptionType", this.ErrorException.GetType().ToString(), WebEventFieldType.String));
                fields.Add(new WebEventFieldData("ExceptionMessage", this.ErrorException.Message, WebEventFieldType.String));
            }
        }

        protected override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
        {
            base.FormatToString(formatter, includeAppInfo);
            if (this._exception != null)
            {
                Exception innerException = this._exception;
                for (int i = 0; (innerException != null) && (i <= 2); i++)
                {
                    formatter.AppendLine(string.Empty);
                    if (i == 0)
                    {
                        formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_exception_information"));
                    }
                    else
                    {
                        formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_inner_exception_information", i.ToString(CultureInfo.InstalledUICulture)));
                    }
                    formatter.IndentationLevel++;
                    formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_exception_type", innerException.GetType().ToString()));
                    formatter.AppendLine(WebBaseEventWrapper.FormatResourceStringWithCache("Webevent_event_exception_message", innerException.Message));
                    formatter.IndentationLevel--;
                    innerException = innerException.InnerException;
                }
            }

        }
    }
}
