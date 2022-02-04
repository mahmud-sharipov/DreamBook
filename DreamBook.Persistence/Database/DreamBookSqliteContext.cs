using Microsoft.EntityFrameworkCore;

namespace DreamBook.Persistence.Database
{
    public class DreamBookSqliteContext : DreamBookBaseContext
    {
        public DreamBookSqliteContext(DbContextOptions<DreamBookSqliteContext> dbContextOptions) : base(dbContextOptions) { }
    }
}
