using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DreamBook.API.Persistence
{
    public class IdentityDreamBookSqlServerContextFactory : 
        DreamBookContextFactoryBase<DreamBookIdentitySqlServerContext>, 
        IDesignTimeDbContextFactory<DreamBookIdentitySqlServerContext>
    {
        protected override DreamBookIdentitySqlServerContext CreateInstance(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookIdentitySqlServerContext>();
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(configuration.GetDBConnectionString(DBProvider.SqlServer));
            return new DreamBookIdentitySqlServerContext(optionsBuilder.Options);
        }
    }

    public class IdentityDreamBookMySqlContexFactory :
        DreamBookContextFactoryBase<DreamBookIdentityMySqlContext>,
        IDesignTimeDbContextFactory<DreamBookIdentityMySqlContext>
    {
        protected override DreamBookIdentityMySqlContext CreateInstance(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookIdentityMySqlContext>();
            optionsBuilder.UseLazyLoadingProxies();
            var connnectionString = configuration.GetDBConnectionString(DBProvider.MySql);
            optionsBuilder.UseMySql(connnectionString, ServerVersion.AutoDetect(connnectionString));
            return new DreamBookIdentityMySqlContext(optionsBuilder.Options);
        }
    }

    public class IdentityDreamBookSqliteContexFactory : 
        DreamBookContextFactoryBase<DreamBookIdentitySqliteContext>,
        IDesignTimeDbContextFactory<DreamBookIdentitySqliteContext>
    {
        protected override DreamBookIdentitySqliteContext CreateInstance(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DreamBookIdentitySqliteContext>();
            optionsBuilder.UseLazyLoadingProxies();
            var connnectionString = configuration.GetDBConnectionString(DBProvider.Sqlite);
            optionsBuilder.UseSqlite(connnectionString);
            return new DreamBookIdentitySqliteContext(optionsBuilder.Options);
        }
    }
}
