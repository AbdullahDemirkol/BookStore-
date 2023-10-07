using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions
{
    internal static class UserExtension
    {
        public static void AddUsers(this BookStoreDbContext context)
        {

            if (!context.Users.Any())
            {
                context.Users.AddRange(GetUsers());
            }
            context.SaveChanges();
        }

        private static List<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Test",
                    Surname = "Test",
                    Email = "Test@gmail.com",
                    Password = "Test",
                }
            };
            return users;
        }
    }
}
