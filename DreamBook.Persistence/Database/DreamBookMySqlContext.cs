using Microsoft.EntityFrameworkCore;

namespace DreamBook.Persistence.Database
{
    public class DreamBookMySqlContext : DreamBookBaseContext
    {
        public DreamBookMySqlContext(DbContextOptions<DreamBookMySqlContext> dbContextOptions) : base(dbContextOptions) { }
    }
}
