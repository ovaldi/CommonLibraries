#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
namespace Kooboo.Common.ObjectContainer
{
    public interface IStartupTask 
    {
        void Execute();

        int Order { get; }
    }
}
