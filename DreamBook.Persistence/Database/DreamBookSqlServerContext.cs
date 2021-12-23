using Microsoft.EntityFrameworkCore;

namespace DreamBook.Persistence.Database
{
    public class DreamBookSqlServerContext : DreamBookBaseContext
    {
        public DreamBookSqlServerContext(DbContextOptions<DreamBookSqlServerContext> dbContextOptions) : base(dbContextOptions) { }
    }
}
