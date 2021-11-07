using DreamBook.Application.Abstraction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DreamBook.API.Infrastructure.Localization
{
    public static class Extensions
    {
        public static IServiceCollection AddAndConfigureLocalization(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(AppLanguageManager.SupportedLanguages[0]);
                options.AddSupportedCultures(AppLanguageManager.SupportedLanguages);
                options.AddSupportedUICultures(AppLanguageManager.SupportedLanguages);
                //options.RequestCultureProviders.Insert(0, new RouteValueRequestCultureProvider() { Options = options });
            });

            return services;
        }
    }
}
