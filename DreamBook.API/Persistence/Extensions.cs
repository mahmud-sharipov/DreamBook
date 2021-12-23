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
        public static IServiceCollection AddUserIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DreamBookIdentityContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetDBConnectionString());
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<DreamBookIdentityContext>();

            return services;
        }

        public static void UpdateDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DreamBookIdentityContext>())
                    context.Database.Migrate();

                using (var context = serviceScope.ServiceProvider.GetService<DreamBookContext>())
                    context.Database.Migrate();
            }
        }
    }
}
