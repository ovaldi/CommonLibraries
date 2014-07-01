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
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using Kooboo.Common.Globalization;
using Kooboo.Common.ComponentModel;

namespace Kooboo.Common.Web.Metadata
{
    public class KoobooDataAnnotationsModelValidatorProvider : System.Web.Mvc.DataAnnotationsModelValidatorProvider
    {
        private class ModelValidatorWrapper : ModelValidator
        {
            public ModelValidatorWrapper(ModelValidator modelValidator, ModelMetadata metadata, ControllerContext controllerContext)
                : base(metadata, controllerContext)
            {
                InnerValidator = modelValidator;
            }
            public ModelValidator InnerValidator { get; private set; }
            public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
            {
                foreach (var item in InnerValidator.GetClientValidationRules())
                {
                    item.ErrorMessage = item.ErrorMessage.Localize();
                    yield return item;
                }

            }
            public override IEnumerable<ModelValidationResult> Validate(object container)
            {
                foreach (var item in InnerValidator.Validate(container))
                {
                    item.Message = item.Message.Localize();
                    yield return item;
                }
            }
        }        
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            foreach (var validator in base.GetValidators(metadata, context, attributes))
            {
                yield return new ModelValidatorWrapper(validator, metadata, context);
            }
        }
        protected override System.ComponentModel.ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            var descriptor = TypeDescriptorHelper.Get(type);
            if (descriptor == null)
            {
                descriptor = base.GetTypeDescriptor(type);
            }
            return descriptor;
        }
    }
}
