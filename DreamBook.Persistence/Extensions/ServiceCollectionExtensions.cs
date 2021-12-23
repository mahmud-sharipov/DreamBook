using DreamBook.Application.Abstraction;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DreamBook.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            switch (configuration.GetDBProvider())
            {
                case DBProvider.SqlServer:
                    service.AddScoped<IContext, DreamBookSqlServerContext>();
                    service.AddScoped<DreamBookBaseContext, DreamBookSqlServerContext>();
                    service.AddDbContext<DreamBookSqlServerContext>(options =>
                    {
                        options.UseLazyLoadingProxies();
                        options.UseSqlServer(configuration.GetDBConnectionString(DBProvider.SqlServer));
                    }, ServiceLifetime.Scoped);
                    break;
                default:
                    service.AddScoped<IContext, DreamBookMySqlContext>();
                    service.AddScoped<DreamBookBaseContext, DreamBookMySqlContext>();
                    service.AddDbContext<DreamBookMySqlContext>(options =>
                    {
                        options.UseLazyLoadingProxies();
                        var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
                        options.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
                    }, ServiceLifetime.Scoped);
                    break;
            }

            return service;
        }
    }
}
