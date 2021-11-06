using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DreamBook.API.Persistence
{
    public class IdentityDreamBookContextFactory : IDesignTimeDbContextFactory<DreamBookIdentityContext>
    {
        public DreamBookIdentityContext CreateDbContext(string[] args) =>
            new DreamBookIdentityContext(new DbContextOptionsBuilder<DreamBookIdentityContext>().Options);
    }
}
