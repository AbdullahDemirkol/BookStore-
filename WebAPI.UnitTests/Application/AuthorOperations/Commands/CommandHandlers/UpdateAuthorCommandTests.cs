using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CommandHandlers
{
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = 99999999;
            command.Model = new UpdateAuthorModel()
            {
                Name = "NewName",
                Surname = "NewSurname",
                DateOfBirth = DateTime.Now.AddYears(-1)
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil");
        }
        [Fact]
        public void WhenValidInputIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = 3;
            command.Model = new UpdateAuthorModel()
            {
                Name = "NewName",
                Surname = "NewSurname",
                DateOfBirth = new DateTime(1991,10,24)
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var author = _dbContext.Authors.FirstOrDefault(g => g.Id==3);
            author.Should().NotBeNull();
            author.Name.Should().Be("NewName");
            author.Surname.Should().Be("NewSurname");
            author.DateOfBirth.Should().Be(new DateTime(1991, 10, 24));
        }
        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange(Hazırlık)
            var authors = _dbContext.Authors.ToList();
            var author = _dbContext.Authors.FirstOrDefault(a => a.Id == 1);
            UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext);
            command.AuthorId = 3;
            command.Model = new UpdateAuthorModel()
            {
                Name = author.Name,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth
            };

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("İsim ve soyisime ait yazar bulunmaktadır");
        }
    }
}
