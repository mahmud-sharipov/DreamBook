using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DreamBook.Persistence.Database
{
    public class DreamBookContextFactory : IDesignTimeDbContextFactory<DreamBookContext>
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
