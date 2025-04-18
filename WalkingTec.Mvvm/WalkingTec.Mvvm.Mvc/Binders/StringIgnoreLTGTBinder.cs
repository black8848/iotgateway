using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace WalkingTec.Mvvm.Mvc.Binders
{
    /// <summary>
    /// StringIgnoreLTGTBinder
    /// 忽略客户端提交的 &lt;及&gt;字符
    /// </summary>
    public class StringIgnoreLTGTBinder : IModelBinder
    {
        /// <summary>
        /// BindModelAsync
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var modelName = bindingContext.ModelName;
            if (string.IsNullOrEmpty(modelName))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            var actDescriptor = bindingContext.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var count = actDescriptor
                .MethodInfo
                .CustomAttributes
                .Where(x => x.AttributeType == typeof(StringNeedLTGTAttribute)).Count();
            var count2 = (bindingContext.ModelMetadata as DefaultModelMetadata)?.Attributes?.PropertyAttributes?
                .Where(x => x.GetType() == typeof(JsonConverterAttribute)).Count();
            if (count == 0 && (count2 == null || count2 == 0))
            {
                value = HttpUtility.HtmlEncode(value);
            }

            bindingContext.Result = ModelBindingResult.Success(value);
            return Task.CompletedTask;
        }
    }
}