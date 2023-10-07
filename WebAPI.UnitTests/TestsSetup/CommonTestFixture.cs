using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.DataAccess;
using WebAPI.Extensions.AutoMapper;
using WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions;

namespace WebAPI.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }
        public CommonTestFixture()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Token:Issuer", "www.BookStore.com"},
                {"Token:Audience", "www.BookStore.com"},
                {"Token:SecurityKey", "This is my private secret key that I use for authentication in the bookstore"},
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context=new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();
            Context.AddUsers();

            Mapper = new MapperConfiguration(configure: cfg => 
            { 
                cfg.AddProfile<MappingProfile>(); 
            }).CreateMapper();

        }
    }
}
