using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CommandHandlers
{
    public class DeleteAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeDeleted()
        {
            //Arrange(Hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = _dbContext.Authors.FirstOrDefault(p=> !_dbContext.Books.Any(b => b.AuthorId == p.Id)).Id;

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var author = _dbContext.Authors.SingleOrDefault(g => g.Id == command.AuthorId);
            author.Should().BeNull();
        }
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");
        }
        [Fact]
        public void WhenBookWithAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId = _dbContext.Books.First().AuthorId;

            var book = _dbContext.Books.Where(p => p.AuthorId == 2).ToList();

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitabı yayında olan bir yazar silinemez. Öncelikle yazarın yayında olan kitapları silinmeli");
        }

    }
}
