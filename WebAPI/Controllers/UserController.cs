using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.TokenOperations.Commands.Models;
using WebAPI.Application.UserOperations.Commands.CommandHandler;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.Application.UserOperations.Commands.Validator;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public UserController(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        //Register
        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_dbContext, _mapper);
            command.Model = newUser;

            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        //Login
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody]CreateTokenModel newToken)
        {
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper,_configuration);
            command.Model = newToken;

            CreateTokenCommandValidator validator=new CreateTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var token = command.Handle();
            return token;
        }
        [HttpGet("refresh/token")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
            command.RefreshToken = refreshToken;

            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var resultToken = command.Handle();
            return resultToken;
        }
    }
}
