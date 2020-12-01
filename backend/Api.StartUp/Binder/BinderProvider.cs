using System;
using Api.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Api.StartUp.Binder
{
    public class BinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(StdReadRequest))
            {
                return new BinderTypeModelBinder(typeof(ReadRequestBinder));
            }

            return null;
        }
    }
}
