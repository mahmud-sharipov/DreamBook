using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DreamBook.API;

public static class Extensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserIdentityService, AuthService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateActor = true,
                    ValidateLifetime = true,
                    ValidateTokenReplay = true,
                    NameClaimType = "name",
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                };
            });

        services.AddAuthorization(auth =>
        {
            auth.DefaultPolicy = new AuthorizationPolicyBuilder(new string[] { JwtBearerDefaults.AuthenticationScheme })
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        return services;
    }

    public static IServiceCollection AddAndConfigureApiVersioning(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");

        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }

    public static IServiceCollection AddAndConfigureLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture(AppLanguageManager.SupportedLanguages[0]);
            options.AddSupportedCultures(AppLanguageManager.SupportedLanguages);
            options.AddSupportedUICultures(AppLanguageManager.SupportedLanguages);
        });

        return services;
    }
}
