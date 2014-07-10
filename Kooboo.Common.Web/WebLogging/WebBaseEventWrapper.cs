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
using System.Web;
using System.Web.Management;

namespace Kooboo.Common.Web.WebLogging
{
    public class WebBaseEventWrapper : WebBaseEvent
    {
        //public WebRequestErrorEventWrapper(Exception e)
        //    : this("", null, WebEventCodes.RuntimeErrorUnhandledException, e)
        //{

        //}
        public WebBaseEventWrapper(string msg,
            object eventSource, EventType eventType) :
            base(msg, eventSource, (int)eventType)
        {
            this.EventType = eventType;
        }

        #region FormatResourceStringWithCache

        public static string FormatResourceStringWithCache(string key)
        {
            var cacheInternal = HttpRuntime.Cache;
            string str2 = CreateWebEventResourceCacheKey(key);
            string str = (string)cacheInternal.Get(str2);
            if (str == null)
            {
                str = SR.GetString(key);
                if (str != null)
                {
                    cacheInternal.Insert(str2, str);
                }
            }
            return str;
        }

        private static string CreateWebEventResourceCacheKey(string key)
        {
            return ("x" + key);
        }
        public static string FormatResourceStringWithCache(string key, string arg0)
        {
            string format = FormatResourceStringWithCache(key);
            if (format == null)
            {
                return null;
            }
            return string.Format(format, arg0);
        }

        #endregion

        internal virtual void GenerateFieldsForMarshal(List<WebEventFieldData> fields)
        {
            fields.Add(new WebEventFieldData("EventTime", this.EventTimeUtc.ToString(), WebEventFieldType.String));
            fields.Add(new WebEventFieldData("EventID", this.EventID.ToString(), WebEventFieldType.String));
            fields.Add(new WebEventFieldData("EventMessage", this.Message, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("ApplicationDomain", ApplicationInformation.ApplicationDomain, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("TrustLevel", ApplicationInformation.TrustLevel, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("ApplicationVirtualPath", ApplicationInformation.ApplicationVirtualPath, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("ApplicationPath", ApplicationInformation.ApplicationPath, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("MachineName", ApplicationInformation.MachineName, WebEventFieldType.String));
            fields.Add(new WebEventFieldData("EventCode", this.EventCode.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
            fields.Add(new WebEventFieldData("EventDetailCode", this.EventDetailCode.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Int));
            fields.Add(new WebEventFieldData("SequenceNumber", this.EventSequence.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Long));
#if MONO
			
#else
            //fields.Add(new WebEventFieldData("Occurrence", this.EventOccurrence.ToString(CultureInfo.InstalledUICulture), WebEventFieldType.Long));
#endif
        }

        protected virtual void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
        {
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_code", this.EventCode.ToString(CultureInfo.InstalledUICulture)));
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_message", this.Message));
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_time", this.EventTime.ToString(CultureInfo.InstalledUICulture)));
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_time_Utc", this.EventTimeUtc.ToString(CultureInfo.InstalledUICulture)));
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_id", this.EventID.ToString("N", CultureInfo.InstalledUICulture)));
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_sequence", this.EventSequence.ToString(CultureInfo.InstalledUICulture)));
#if MONO
				
#else
            //formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_occurrence", this.EventOccurrence.ToString(CultureInfo.InstalledUICulture)));
#endif
            formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_detail_code", this.EventDetailCode.ToString(CultureInfo.InstalledUICulture)));
            if (includeAppInfo)
            {
                formatter.AppendLine(string.Empty);
                formatter.AppendLine(FormatResourceStringWithCache("Webevent_event_application_information"));
                formatter.IndentationLevel++;
                ApplicationInformation.FormatToString(formatter);
                formatter.IndentationLevel--;
            }
        }

        internal bool IsSystemEvent
        {
            get
            {
                return (this.EventCode < 0x186a0);
            }
        }

        public EventType EventType { get; set; }

        public override string ToString()
        {
            return this.ToString(true, true);
        }
        public override string ToString(bool includeAppInfo, bool includeCustomEventDetails)
        {
            WebEventFormatter formatter = new WebEventFormatter();
            this.FormatToString(formatter, includeAppInfo);
            if (!this.IsSystemEvent && includeCustomEventDetails)
            {
                formatter.AppendLine(string.Empty);
                formatter.AppendLine(SR.GetString("Webevent_event_custom_event_details"));
                formatter.IndentationLevel++;
                this.FormatCustomEventDetails(formatter);
                formatter.IndentationLevel--;
            }
            return formatter.ToString();
        }

        public virtual void FormatCustomEventDetails(WebEventFormatter formatter)
        {
        }
    }
}
