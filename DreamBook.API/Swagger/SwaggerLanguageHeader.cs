using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DreamBook.API.Swagger
{
    public class SwaggerLanguageHeader : IOperationFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public SwaggerLanguageHeader(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            var requestLocalizationOptions = (_serviceProvider.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>)?
                    .Value;
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                Description = "Supported languages",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = requestLocalizationOptions?.SupportedCultures?.Select(c => new OpenApiString(c.TwoLetterISOLanguageName)).ToList<IOpenApiAny>(),
                    Default = new OpenApiString(requestLocalizationOptions?.DefaultRequestCulture.Culture.TwoLetterISOLanguageName ?? ""),
                },
            });
        }
    }
}
