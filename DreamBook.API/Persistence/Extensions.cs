using DreamBook.API.Auth.Model;
using DreamBook.Persistence.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DreamBook.API.Persistence
{
    public static class Extensions
    {
        public static void UpdateDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DreamBookIdentityBaseContext>())
                    context.Database.Migrate();

                using (var context = serviceScope.ServiceProvider.GetService<DreamBookBaseContext>())
                    context.Database.Migrate();
            }
        }

        public static IServiceCollection AddUserIdentity(this IServiceCollection services, IConfiguration configuration)
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

        static void AddMySql(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DreamBookIdentityBaseContext, DreamBookIdentityMySqlContext>();
            services.AddDbContext<DreamBookIdentityMySqlContext>(options =>
            {
                options.UseLazyLoadingProxies();
                var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
                options.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
            }, ServiceLifetime.Scoped);
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DreamBookIdentityMySqlContext>();
        }

        static void AddSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DreamBookIdentityBaseContext, DreamBookIdentitySqlServerContext>();
            services.AddDbContext<DreamBookIdentitySqlServerContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetDBConnectionString(DBProvider.SqlServer));
            }, ServiceLifetime.Scoped);
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DreamBookIdentitySqlServerContext>();
        }

        static void AddSqlite(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DreamBookIdentityBaseContext, DreamBookIdentitySqliteContext>();
            services.AddDbContext<DreamBookIdentitySqliteContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(configuration.GetDBConnectionString(DBProvider.Sqlite));
            }, ServiceLifetime.Scoped);
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DreamBookIdentitySqliteContext>();
        }
    }
}
