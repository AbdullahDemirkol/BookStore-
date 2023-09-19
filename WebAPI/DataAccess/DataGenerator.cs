using Microsoft.EntityFrameworkCore;
using WebAPI.Entity.Concrete;
using WebAPI.Entity.Enum;

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
                context.Books.AddRange(GetBooks());
                context.SaveChanges();
            }
        }

        private static List<Book> GetBooks()
        {
            var books= new List<Book>(){
                new Book()
                {
                    //Id = 1,
                    Title = "Lean Startup",
                    GenreId = GenreEnum.ScienceFiction,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book()
                {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = GenreEnum.PersonalGrowth,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book()
                {
                    //Id = 3,
                    Title = "Dune",
                    GenreId = GenreEnum.Noval,
                    PageCount = 540,
                    PublishDate = new DateTime(2006, 12, 21)
                } 
            };
            return books;
        }
    }
}
