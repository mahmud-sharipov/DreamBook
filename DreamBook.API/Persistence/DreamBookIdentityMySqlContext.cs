using Microsoft.EntityFrameworkCore;

namespace DreamBook.API.Persistence
{

    public class DreamBookIdentityMySqlContext : DreamBookIdentityBaseContext
    {
        public DreamBookIdentityMySqlContext(DbContextOptions<DreamBookIdentityMySqlContext> options) : base(options)
        {
        }
    }
}