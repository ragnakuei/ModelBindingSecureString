using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBindingSecureString.Infra
{
    public class SecureStringBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            var value = valueProviderResult.FirstValue;

            var propertyValue = new SecureString();

            foreach (var c in value)
            {
                propertyValue.AppendChar(c);
            }

            bindingContext.Result = ModelBindingResult.Success(propertyValue);

            return Task.CompletedTask;
        }
    }
}