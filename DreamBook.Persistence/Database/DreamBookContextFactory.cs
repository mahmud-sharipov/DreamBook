using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace DreamBook.Persistence.Database
{
    public interface IDreamBookContextFactory
    {
        public IContext CreateDbContext();
    }

    public abstract class DreamBookContextFactoryBase<TContext> : IDreamBookContextFactory where TContext : DbContext
    {
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

        IContext IDreamBookContextFactory.CreateDbContext() => (IContext)CreateDbContext(null);
    }

    public class DreamBookMySqlContextFactory : DreamBookContextFactoryBase<DreamBookMySqlContext>, IDesignTimeDbContextFactory<DreamBookMySqlContext>
    {
        protected override DreamBookMySqlContext CreateInstance(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookMySqlContext>();
            optionsBuilder.UseLazyLoadingProxies();
            var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
            optionsBuilder.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
            return new DreamBookMySqlContext(optionsBuilder.Options);
        }
    }

    public class DreamBookSqlServerContextFactory : DreamBookContextFactoryBase<DreamBookSqlServerContext>, IDesignTimeDbContextFactory<DreamBookSqlServerContext>
    {
        protected override DreamBookSqlServerContext CreateInstance(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookSqlServerContext>();
            optionsBuilder.UseLazyLoadingProxies();
            var connnectionString = configuration.GetDBConnectionString(DBProvider.SqlServer);
            optionsBuilder.UseSqlServer(connnectionString);
            return new DreamBookSqlServerContext(optionsBuilder.Options);
        }
    }
}
