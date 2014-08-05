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

namespace Kooboo.Common.Web.Button
{
    public class GroupedButton
    {
        public GroupedButton(IButton group, IEnumerable<IButton> buttons)
        {
            this.Group = group;
            this.Buttons = buttons;
        }
        public IButton Group { get; private set; }

        public IEnumerable<IButton> Buttons { get; private set; }
    }
}
