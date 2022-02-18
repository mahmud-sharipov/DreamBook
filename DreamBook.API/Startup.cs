using DreamBook.API.Filters;
using DreamBook.API.Import;
using DreamBook.API.Middleware;
using DreamBook.API.Swagger;

namespace DreamBook.API;

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
        AddServices(services);

        services.AddCors();
        services.AddAndConfigureLocalization();

        services.AddMvc(option =>
        {
            option.EnableEndpointRouting = false;
            option.Filters.Add<ValidationFilter>();
        }).AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<ApplicationValidatorEntryPoint>();
            options.RegisterValidatorsFromAssemblyContaining<Startup>();
        });

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        //services.AddHttpsRedirection(op => op.RedirectStatusCode = 307);
        services.AddRouting(options => options.LowercaseUrls = true);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddHttpContextAccessor();

        services.AddAndConfigureApiVersioning();
        services.AddAndConfigureSwagger();
        services.AddApiAuthentication(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseMiddleware<ApiExceptionHandlingMiddleware>();

        app.UseSwaggerDoc(versionProvider);

        app.UseStaticFiles();
        app.UseDefaultFiles();

        app.UseRequestLocalization();
        //app.UseHttpsRedirection();

        app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

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

    void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

    }
}
