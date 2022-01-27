using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DreamBook.Persistence.Database
{
    public class DreamBookContextFactory: IDesignTimeDbContextFactory<DreamBookContext>
    {

        public DreamBookContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"dbsettings.Development.json", optional: true)
                  .AddJsonFile($"appsettings.json", optional: true)
                  .AddEnvironmentVariables()
                  .Build();

            return new DreamBookContext(configuration);
        }
    }
}
