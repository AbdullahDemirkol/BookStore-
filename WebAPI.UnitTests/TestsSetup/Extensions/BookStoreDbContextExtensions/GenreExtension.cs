using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions
{
    internal static class GenreExtension
    {
        public static void AddGenres(this BookStoreDbContext context)
        {

            if (!context.Genres.Any())
            {
                context.Genres.AddRange(GetGenres());
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
    }
}
