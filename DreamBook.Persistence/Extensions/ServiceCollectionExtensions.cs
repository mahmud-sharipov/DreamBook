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
            service.AddScoped<IContext, DreamBookContext>();
            service.AddDbContext<DreamBookContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetDBConnectionString());
            }, ServiceLifetime.Scoped);

            return service;
        }
    }
}
