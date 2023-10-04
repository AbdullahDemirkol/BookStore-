using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Extensions.AutoMapper;
using WebAPI.UnitTests.TestsSetup.Extensions.BookStoreDbContextExtensions;

namespace WebAPI.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context=new BookStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddGenres();
            Context.AddAuthors();
            Context.AddBooks();

            Mapper = new MapperConfiguration(configure: cfg => 
            { 
                cfg.AddProfile<MappingProfile>(); 
            }).CreateMapper();
        }
    }
}
