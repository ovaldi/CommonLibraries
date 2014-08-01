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
    public class LoadedFormTabPlugin
    {
        public LoadedFormTabPlugin(IFormTabPlugin tabPlugin, FormTabContext cotnext)
        {
            this.TabPlugin = tabPlugin;
            this.Context = cotnext;
        }
        public IFormTabPlugin TabPlugin { get; private set; }
        public FormTabContext Context { get; private set; }
    }
}
