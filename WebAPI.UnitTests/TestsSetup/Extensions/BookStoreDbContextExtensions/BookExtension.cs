using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions
{
    internal static class BookExtensions
    {
        public static void AddBooks(this BookStoreDbContext context)
        {

            if (!context.Books.Any())
            {
                context.Books.AddRange(GetBooks(context));
            }
            context.SaveChanges();
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
