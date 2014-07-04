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
using System.Net;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using Kooboo.Common.Caching;
using Kooboo.Common.Web;
namespace Kooboo.Common.CachingSync.Http
{
    [Kooboo.Common.ObjectContainer.Dependency.Dependency(typeof(ICacheExpiredNotifier), Key = "HttpNotifier")]
    public class HttpNotifier : ICacheExpiredNotifier
    {
        public void Notify(string objectCacheName, string key)
        {
            var servers = CacheNotificationSection.GetSection();
            if (servers != null)
            {
                foreach (ServerItemElement item in servers.Servers)
                {
                    ProcessNotification(item, objectCacheName, key);
                }
            }
        }
        private void ProcessNotification(ServerItemElement server, string objectCacheName, string key)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    System.Threading.Thread.Sleep(server.Delay * 1000);
                    SendRequest(server.Url, objectCacheName, key);
                }
                catch (Exception e)
                {
                    Kooboo.Common.Logging.Logger.Error(e.Message, e);
                }
            });
            thread.Start();
        }
        private void SendRequest(string url, string objectCacheName, string key)
        {
            NameValueCollection queryString = new NameValueCollection();
            queryString["ObjectCacheName"] = objectCacheName;
            queryString["CacheKey"] = key;
            queryString["_ts"] = DateTime.Now.Ticks.ToString();
            queryString["source"] = AppDomain.CurrentDomain.FriendlyName;
            url = CombineQueryString(url, queryString);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
        }
        private static string CombineQueryString(string url, NameValueCollection queryString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in queryString.AllKeys)
            {
                sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(queryString[key]));
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return UrlUtility.CombineQueryString(url, sb.ToString());
        }
    }
}
