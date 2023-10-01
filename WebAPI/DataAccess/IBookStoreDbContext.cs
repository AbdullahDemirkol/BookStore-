using Microsoft.EntityFrameworkCore;
using WebAPI.Entity.Concrete;

namespace WebAPI.DataAccess
{
    public interface IBookStoreDbContext
    {
        int SaveChanges();
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Book> Books { get; set; }

    }
}
