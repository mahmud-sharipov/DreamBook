using Microsoft.EntityFrameworkCore;

namespace DreamBook.API.Persistence
{
    public class DreamBookIdentitySqliteContext : DreamBookIdentityBaseContext
    {
        public DreamBookIdentitySqliteContext(DbContextOptions<DreamBookIdentitySqliteContext> options) : base(options)
        {
        }
    }
}