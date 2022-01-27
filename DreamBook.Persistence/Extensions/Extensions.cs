using DreamBook.Application.Abstraction;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configuration)
    {
       // service.AddScoped<IContext, DreamBookContext>();
        service.AddDbContext<DreamBookContext>(ServiceLifetime.Scoped);
        return service;
    }

    public static void SetupProviderOptions(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
    {
        optionsBuilder.UseLazyLoadingProxies();
        var provider = configuration.GetDBProvider();
        var connnectionString = configuration.GetDBConnectionString(provider);

        if (provider == DBProvider.MySql)
            optionsBuilder.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
        else if (provider == DBProvider.SqlServer)
            optionsBuilder.UseSqlServer(connnectionString);
    }

}
