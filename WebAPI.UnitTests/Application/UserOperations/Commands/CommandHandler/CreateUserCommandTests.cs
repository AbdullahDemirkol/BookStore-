using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.UserOperations.Commands.CommandHandler;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.UnitTests.TestSetup;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.CommandHandler
{
    public class CreateUserCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_User_ShouldBeCreated()
        {
            //Arrange(Hazırlık)
            CreateUserCommand command = new CreateUserCommand(_dbContext,_mapper);
            command.Model = new CreateUserModel()
            {
                Name = "Abdullah",
                Surname = "Demirkol",
                Email = "abdullah@gmail.com",
                Password = "12345",
                
            };


            //Act (Çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert (Doğrulama)
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == "abdullah@gmail.com");
            user.Should().NotBeNull();
            user.Id.Should().BeGreaterThan(0);
            user.Name.Should().NotBeNull();
            user.Surname.Should().NotBeNull();
            user.Email.Should().NotBeNull();
            user.Password.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyExistEmailIsGiven_InvalidOperationException_ShouldBeReturned()
        {

            //Arrange(Hazırlık)
            CreateUserCommand command = new CreateUserCommand(_dbContext, _mapper);
            command.Model = new CreateUserModel()
            {
                Name = "Abdullah",
                Surname = "Demirkol",
                Email = "Test@gmail.com",
                Password = "12345",
            };
            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı zaten mevcut");
            
        }
    }
}
