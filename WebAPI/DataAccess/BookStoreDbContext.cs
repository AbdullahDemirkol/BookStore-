using Microsoft.EntityFrameworkCore;

namespace WebAPI.DataAccess
{
    public class BookStoreDbContext:DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
