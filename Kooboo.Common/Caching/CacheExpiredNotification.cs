#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
#region usings

using Kooboo.Common.Logging;
using System;
using System.Collections.Generic;

#endregion

namespace Kooboo.Common.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public static class CacheExpiredNotification
    {
        private static IEnumerable<ICacheExpiredNotifier> GetNotifiers()
        {
            return Kooboo.Common.ObjectContainer.EngineContext.Current.ResolveAll<ICacheExpiredNotifier>();
        }
        #region Methods
        /// <summary>
        /// Notifies the specified object cache name.
        /// </summary>
        /// <param name="objectCacheName">Name of the object cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        public static void Notify(string objectCacheName, string cacheKey)
        {
            var notifiers = GetNotifiers();
            if (notifiers != null)
            {
                try
                {
                    foreach (var item in notifiers)
                    {
                        item.Notify(objectCacheName, cacheKey);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message, e);
                }

            }
        }
        #endregion
    }
}
