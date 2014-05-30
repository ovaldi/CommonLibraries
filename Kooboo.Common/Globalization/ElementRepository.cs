#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Globalization.Repository;

namespace Kooboo.Common.Globalization
{
    /// <summary>
    /// 
    /// </summary>
    public static class ElementRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public static IElementRepository DefaultRepository = new CacheElementRepository(() => new XmlElementRepository());
    }
}
