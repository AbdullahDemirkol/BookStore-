using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions
{
    internal static class AuthorExtension
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            if (!context.Authors.Any())
            {
                context.Authors.AddRange(GetAuthors());
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
    }
}
