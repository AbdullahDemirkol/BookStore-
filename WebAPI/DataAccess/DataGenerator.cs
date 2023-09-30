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
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(GetGenres());
                }
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(GetAuthors());
                    context.SaveChanges();
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(GetBooks(context));
                }
                context.SaveChanges();
            }
        }

        private static List<Author> GetAuthors()
        {
            var authors = new List<Author>()
            {
                new Author()
                {
                    Name= "Eric",
                    Surname="Ries",
                    DateOfBirth= new DateTime(1978,9,22)
                },
                new Author()
                {
                    Name= "Charlotte Perkins",
                    Surname="Gilman",
                    DateOfBirth= new DateTime(1860,7,3)
                },
                new Author()
                {
                    Name= "Frank",
                    Surname="Herbert",
                    DateOfBirth= new DateTime(1920,10,8)
                }
            };
            return authors;
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

        private static List<Book> GetBooks(BookStoreDbContext context)
        {
            var books = new List<Book>(){
                new Book()
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = 1,
                    AuthorId = context.Authors.FirstOrDefault(p=>p.Name=="Eric"&&p.Surname=="Ries").Id,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book()
                {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2,
                    AuthorId = context.Authors.FirstOrDefault(p => p.Name == "Charlotte Perkins" && p.Surname == "Gilman").Id,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book()
                {
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 3,
                    AuthorId = context.Authors.FirstOrDefault(p => p.Name == "Frank" && p.Surname == "Herbert").Id,
                    PageCount = 540,
                    PublishDate = new DateTime(2006, 12, 21)
                }
            };
            return books;
        }
    }
}
