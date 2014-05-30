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
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer.MEF
{
    public static class PartBuilderExtensions
    {
        public static PartBuilder LifeStyle(this PartBuilder pb, ComponentLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case ComponentLifeStyle.Singleton:
                    pb = pb.SetCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared);
                    break;
                case ComponentLifeStyle.InRequestScope:
                case ComponentLifeStyle.InThreadScope:
                    throw new NotSupportedFeaturesException();
                case ComponentLifeStyle.Transient:
                default:
                    pb = pb.SetCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared);
                    break;
            }

            return pb;
        }
    }
}
