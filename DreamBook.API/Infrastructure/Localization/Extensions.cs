using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace DreamBook.API.Infrastructure.Localization
{
    public static class Extensions
    {
        public static IServiceCollection AddAndConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            var supportedCultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("ru") };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(supportedCultures[1]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                //options.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider() { Options = options });
            });

            return services;
        }
    }
}
