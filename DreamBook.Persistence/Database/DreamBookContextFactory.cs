using Microsoft.EntityFrameworkCore;

namespace DreamBook.Persistence.Database
{
    public class DreamBookContextFactory : DreamBookContextFactoryBase<DreamBookContext>
    {
        protected override DreamBookContext CreateInstance(DbContextOptions<DreamBookContext> contextOptionsBuilder) =>
            new DreamBookContext(contextOptionsBuilder);
    }
}
