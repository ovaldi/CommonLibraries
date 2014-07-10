#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Data.DataViolation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class ModelStateExtensionMethods
    {
        public static void FillDataViolation(this ModelStateDictionary modelState, IEnumerable<DataViolationItem> violations)
        {
            foreach (var issue in violations)
            {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }

    }
}