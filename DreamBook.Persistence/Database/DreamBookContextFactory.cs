using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DreamBook.Persistence.Database
{
    public class DreamBookContextFactory : IDesignTimeDbContextFactory<DreamBookContext>
    {
        public DreamBookContext CreateDbContext(string[] args) =>
            new DreamBookContext(new DbContextOptionsBuilder<DreamBookContext>().Options);
    }
}
