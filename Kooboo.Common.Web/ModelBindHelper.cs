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
using System.Web.Mvc;

namespace Kooboo.Common.Web
{
    public static class ModelBindHelper
    {
        public static T BindModel<T>(T model, ControllerContext controllerContext)
        {
            return BindModel(model, "", controllerContext);
        }
        public static T BindModel<T>(T model, string prefix, ControllerContext controllerContext)
        {
            IModelBinder binder = ModelBinders.Binders.GetBinder(typeof(T));
            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(T)),
                ModelName = prefix,
                ModelState = controllerContext.Controller.ViewData.ModelState,
                PropertyFilter = null,
                ValueProvider = controllerContext.Controller.ValueProvider
            };
            return (T)binder.BindModel(controllerContext, bindingContext);
        }
        public static object BindModel(Type modelType, ControllerContext controllerContext)
        {
            return BindModel(modelType, "", controllerContext);
        }
        public static object BindModel(Type modelType, string prefix, ControllerContext controllerContext)
        {
            IModelBinder binder = ModelBinders.Binders.GetBinder(modelType);
            ModelBindingContext bindingContext = new ModelBindingContext
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => null, modelType),
                ModelName = prefix,
                ModelState = controllerContext.Controller.ViewData.ModelState,
                PropertyFilter = null,
                ValueProvider = controllerContext.Controller.ValueProvider
            };
            return binder.BindModel(controllerContext, bindingContext);
        }
    }
}
