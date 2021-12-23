using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DreamBook.Persistence.Database
{
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
