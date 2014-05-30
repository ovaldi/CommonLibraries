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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer.MEF
{
    public static class CompositionContainerExtensions
    {
        public static object GetExportedValue(this CompositionContainer container, Type type, string contractName = null)
        {
            var export = container.GetExports(type, null, contractName).FirstOrDefault();
            if (export == null)
            {
                throw new ImportCardinalityMismatchException();
            }
            return export.Value;
        }
        public static object GetExportedValueOrDefault(this CompositionContainer container, Type type, string contractName = null)
        {
            var export = container.GetExports(type, null, contractName).FirstOrDefault();
            if (export == null)
            {
                return null;
            }
            return export.Value;
        }
        public static object[] GetExportedValues(this CompositionContainer container, Type type, string contractName = null)
        {
            var exports = container.GetExports(type, null, contractName);
            return exports.Select(it => it.Value).ToArray();
        }
    }
}
