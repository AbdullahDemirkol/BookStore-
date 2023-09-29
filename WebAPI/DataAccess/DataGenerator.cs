using Microsoft.EntityFrameworkCore;
using WebAPI.Entity.Concrete;

namespace WebAPI.DataAccess
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context=new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(GetGenres());
                context.Books.AddRange(GetBooks());
                context.SaveChanges();
            }
        }

        private static List<Genre> GetGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Name="Personal Growth"
                },
                new Genre()
                {
                    Name="Science Fiction"
                },
                new Genre()
                {
                    Name="Noval"
                },
                new Genre()
                {
                    Name="Romance"
                }
            };
            return genres;
        }

        private static List<Book> GetBooks()
        {
            var books= new List<Book>(){
                new Book()
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book()
                {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book()
                {
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 3,
                    PageCount = 540,
                    PublishDate = new DateTime(2006, 12, 21)
                } 
            };
            return books;
        }
    }
}
