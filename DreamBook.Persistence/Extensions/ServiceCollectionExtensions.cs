using DreamBook.Persistence.Services;
using Microsoft.AspNetCore.Builder;

namespace DreamBook.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dp = configuration.GetDBProvider();
        if (dp == DBProvider.SqlServer)
            AddSqlServer(services, configuration);
        else if (dp == DBProvider.MySql)
            AddMySql(services, configuration);
        else if (dp == DBProvider.Sqlite)
            throw new NotSupportedException("Sqlite does bot support!");

        services.AddAutoMapper(typeof(UserMappingProfile));
        return services;
    }

    private static void AddMySql(IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<IContext, DreamBookMySqlContext>();
        service.AddScoped<IDreamBookContextFactory, DreamBookMySqlContextFactory>();
        service.AddScoped<DreamBookBaseContext, DreamBookMySqlContext>();
        service.AddDbContext<DreamBookMySqlContext>(options =>
        {
            options.UseLazyLoadingProxies();
            var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
            options.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
        }, ServiceLifetime.Scoped);

        service.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<DreamBookMySqlContext>();
    }

    private static void AddSqlServer(IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<IContext, DreamBookSqlServerContext>();
        service.AddScoped<IDreamBookContextFactory, DreamBookSqlServerContextFactory>();
        service.AddScoped<DreamBookBaseContext, DreamBookSqlServerContext>();

        service.AddDbContext<DreamBookSqlServerContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(configuration.GetDBConnectionString(DBProvider.SqlServer));
        }, ServiceLifetime.Scoped);

        service.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<DreamBookSqlServerContext>();
    }

    public static void UpdateDatabase(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<DreamBookBaseContext>();
        context.Database.Migrate();
    }
}
