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

namespace Kooboo.Common.TokenTemplate
{
    /// <summary>
    /// Refact from Kooboo.CMS.Common.Formula.IFormulaParser
    /// </summary>
    public interface ITokenTemplate
    {
        string Merge(string template, IValueProvider valueProvider);

        IEnumerable<string> GetTokens(string template);
    }
}
