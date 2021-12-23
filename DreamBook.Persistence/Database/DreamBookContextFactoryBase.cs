using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace DreamBook.Persistence.Database
{
    public abstract class DreamBookContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"dbsettings.{ Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookContext>();

            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(configuration.GetDBConnectionString());
            return CreateInstance(new DbContextOptionsBuilder<TContext>().Options);
        }

        protected abstract TContext CreateInstance(DbContextOptions<TContext> contextOptionsBuilder);
    }
}
