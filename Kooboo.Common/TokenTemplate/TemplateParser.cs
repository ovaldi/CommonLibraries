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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kooboo.Common.TokenTemplate
{
    /// <summary>    
    /// Refact from Kooboo.CMS.Common.Formula.FormulaParser
    /// </summary>    
    public class TemplateParser : ITemplateParser
    {
        public virtual string Merge(string template, IValueProvider valueProvider)
        {
            if (string.IsNullOrEmpty(template))
            {
                return null;
            }
            var matches = Regex.Matches(template, "{(?<Name>[^{^}]+)}");
            var s = template;
            foreach (Match match in matches)
            {
                var key = match.Groups["Name"].Value;
                int intKey;
                //ignore {0},{1}... it is the format string.
                if (!int.TryParse(key, out intKey))
                {
                    var value = valueProvider.GetValue(match.Groups["Name"].Value);
                    var htmlValue = value == null ? "" : value.ToString();
                    s = s.Replace(match.Value, htmlValue);
                }

            }
            return s;
        }


        public IEnumerable<string> GetTokens(string template)
        {
            List<string> parameters = new List<string>();
            if (!string.IsNullOrEmpty(template))
            {
                var matches = Regex.Matches(template, "{(?<Name>[^{^}]+)}");

                foreach (Match match in matches)
                {
                    var parameter = match.Groups["Name"].Value;
                    if (!parameters.Contains(parameter, StringComparer.OrdinalIgnoreCase))
                    {
                        parameters.Add(parameter);
                    }

                }
            }
            return parameters;
        }
    }
}
