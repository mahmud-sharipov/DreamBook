using Microsoft.EntityFrameworkCore;

namespace DreamBook.API.Persistence
{
    public class DreamBookIdentitySqlServerContext : DreamBookIdentityBaseContext
    {
        public DreamBookIdentitySqlServerContext(DbContextOptions<DreamBookIdentitySqlServerContext> options) : base(options)
        {
        }
    }
}