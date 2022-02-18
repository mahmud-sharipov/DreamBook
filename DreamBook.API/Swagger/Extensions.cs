using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DreamBook.API.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<SwaggerLanguageHeader>();
                options.EnableAnnotations();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static void UseSwaggerDoc(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.UseSwagger(options => { options.RouteTemplate = "api/docs/{documentName}/docs.json"; });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api/docs";
                options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                foreach (var description in versionProvider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/api/docs/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
            });
        }
    }
}
