using DreamBook.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace DreamBook.API.Persistence
{
    public class IdentityDreamBookContextFactory : DreamBookContextFactoryBase<DreamBookIdentityContext>
    {
        protected override DreamBookIdentityContext CreateInstance(DbContextOptions<DreamBookIdentityContext> contextOptionsBuilder) =>
            new DreamBookIdentityContext(contextOptionsBuilder);
    }
}
