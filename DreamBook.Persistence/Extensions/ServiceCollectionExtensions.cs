using DreamBook.Application.Abstraction;
using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DreamBook.Persistence.Extensions
{
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
                AddSqlite(services, configuration);

            return services;
        }

        private static void AddMySql(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IContext, DreamBookMySqlContext>();
            service.AddScoped<DreamBookBaseContext, DreamBookMySqlContext>();
            service.AddDbContext<DreamBookMySqlContext>(options =>
            {
                options.UseLazyLoadingProxies();
                var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
                options.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
            }, ServiceLifetime.Scoped);
        }

        private static void AddSqlServer(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IContext, DreamBookSqlServerContext>();
            service.AddScoped<DreamBookBaseContext, DreamBookSqlServerContext>();
            service.AddDbContext<DreamBookSqlServerContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetDBConnectionString(DBProvider.SqlServer));
            }, ServiceLifetime.Scoped);
        }

        static void AddSqlite(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContext, DreamBookSqliteContext>();
            services.AddScoped<DreamBookBaseContext, DreamBookSqliteContext>();
            services.AddDbContext<DreamBookSqliteContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(configuration.GetDBConnectionString(DBProvider.Sqlite));
            }, ServiceLifetime.Scoped);
        }
    }
}
