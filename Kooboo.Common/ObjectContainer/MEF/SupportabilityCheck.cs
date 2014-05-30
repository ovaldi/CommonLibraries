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

namespace Kooboo.Common.ObjectContainer.MEF
{
    public static class SupportabilityCheck
    {
        public static void CheckParameters(Parameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                throw new NotSupportedFeaturesException();
            }
        }
    }
}
