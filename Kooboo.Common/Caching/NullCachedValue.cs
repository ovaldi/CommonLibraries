#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion

namespace Kooboo.Common.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class NullCachedValue
    {
        #region .ctor
        public NullCachedValue()
        {

        } 
        #endregion

        #region statics
        public volatile static NullCachedValue Value = new NullCachedValue(); 
        #endregion

        #region Methods
        public override bool Equals(object obj)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public static bool operator ==(NullCachedValue obj1, NullCachedValue obj2)
        {
            return true;
        }
        public static bool operator !=(NullCachedValue obj1, NullCachedValue obj2)
        {
            return false;
        } 
        #endregion
    }
}
