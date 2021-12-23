using DreamBook.API.Auth;
using DreamBook.API.Infrastructure.ApiVersioning;
using DreamBook.API.Infrastructure.Filters;
using DreamBook.API.Infrastructure.Localization;
using DreamBook.API.Infrastructure.Middleware;
using DreamBook.API.Infrastructure.Swagger;
using DreamBook.API.Persistence;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Extensions;
using DreamBook.Persistence.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DreamBook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(Configuration);

            services.AddCors();
            services.AddAndConfigureLocalization();

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add<ValidationFilter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<ApplicationValidatorEntryPoint>();
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddHttpsRedirection(op => op.RedirectStatusCode = 307);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

            services.AddAndConfigureApiVersioning();
            services.AddAndConfigureSwagger();

            services.AddUserIdentity(Configuration);

            services.AddApiAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<ApiExceptionHandlingMiddleware>();

            app.UseSwaggerDoc(versionProvider);

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRequestLocalization();
            app.UseHttpsRedirection();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            app.UpdateDatabase();
            SeedDatabase.Seed(app).Wait();
        }
    }
}
