using AutoMapper;
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
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Author_BeShouldCreated()
        {
            //Arrange(Hazırlık)
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "NewName",
                Surname= "NewSurname",
                DateOfBirth= DateTime.Today.AddYears(-1),
            };

            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var author = _dbContext.Authors.FirstOrDefault(g => g.Name == "NewName" && g.Surname == "NewSurname" && g.DateOfBirth == DateTime.Today.AddYears(-1));
            author.Should().NotBeNull();
            author.Id.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenAlreadyExistGenreIsGiven_InvalidOperationException_BeShouldReturn()
        {
            //Arrange(Hazırlık)
            CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
            command.Model = new CreateAuthorModel()
            {
                Name = "NewName",
                Surname = "NewSurname",
                DateOfBirth = DateTime.Today.AddYears(-1)
            };

            //Act & Assert (Çalıştırma  - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }


    }
}
