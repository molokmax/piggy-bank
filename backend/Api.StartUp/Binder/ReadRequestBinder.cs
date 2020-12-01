using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.StartUp.Binder
{
    public class ReadRequestBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            /*
             
                Filter = bindingContext.ValueProvider.GetValue("$filter").FirstValue,
                Expand = bindingContext.ValueProvider.GetValue("$expand").FirstValue,
                Select = bindingContext.ValueProvider.GetValue("$select").FirstValue,
                Skip = bindingContext.ValueProvider.GetValue("$skip").FirstValue,
                Top = bindingContext.ValueProvider.GetValue("$top").FirstValue,
                OrderBy = bindingContext.ValueProvider.GetValue("$orderby").FirstValue,
                InlineCount = bindingContext.ValueProvider.GetValue("$inlinecount").FirstValue,
                Search = bindingContext.ValueProvider.GetValue("$search").FirstValue,
                FilterOption = bindingContext.ValueProvider.GetValue("$filterOption").FirstValue,
                SearchType = bindingContext.ValueProvider.GetValue("$searchType").FirstValue,
                GridFields = bindingContext.ValueProvider.GetValue("$gridFields").FirstValue,
            */

            var result = new StdReadRequest()
            {
                Order = new List<Model.Base.Sorting>(),
                Where = new List<Model.Base.Filter>()
            };
            bindingContext.Result = ModelBindingResult.Success(result);

            return Task.CompletedTask;
        }
    }
}
