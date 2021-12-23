using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DreamBook.Persistence.Database
{
    public abstract class DreamBookContextFactoryBase<TContext> where TContext : DbContext
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"dbsettings.Development.json", optional: true)
                  .AddJsonFile($"appsettings.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();

            return CreateInstance(configuration);
        }

        protected abstract TContext CreateInstance(IConfiguration configuration);
    }
}
