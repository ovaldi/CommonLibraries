#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.ObjectContainer.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer.MEF
{
    public class MEFEngine : EngineBase, IEngine
    {
        #region Ctor
        public MEFEngine()
            : this(new AppDomainTypeFinder())
        {

        }
        public MEFEngine(ITypeFinder typeFinder)
            : this(typeFinder, new MEFContainerManager())
        { }
        public MEFEngine(ITypeFinder typeFinder, MEFContainerManager containerManager)
            : base(typeFinder, containerManager)
        {


        }
        #endregion




    }
}
