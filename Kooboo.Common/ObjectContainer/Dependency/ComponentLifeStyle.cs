#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
namespace Kooboo.Common.ObjectContainer.Dependency
{
    public enum ComponentLifeStyle
    {
        Singleton = 0,
        Transient = 1,
        InThreadScope = 2,
        InRequestScope = 3
    }
}
