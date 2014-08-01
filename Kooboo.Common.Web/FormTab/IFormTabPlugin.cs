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
using System.Threading.Tasks;

namespace Kooboo.Common.Web.FormTab
{
    public interface IFormTabPlugin : IApplyTo
    {
        string Name { get; }
        string DisplayText { get; }
        string ViewVirtualPath { get; }

        bool Order { get; }

        Type ModelType { get; }

        void LoadData(FormTabContext context);

        void Submit(FormTabContext context);
    }
}
