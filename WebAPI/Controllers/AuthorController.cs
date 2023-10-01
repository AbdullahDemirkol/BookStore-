using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.AuthorOperations.Commands.CommandHandler;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.Application.AuthorOperations.Commands.Validator;
using WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthor;
using WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthors;
using WebAPI.Application.AuthorOperations.Queries.QueryViewModel;
using WebAPI.Application.AuthorOperations.Queries.Validator;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_dbContext, _mapper);
            var result=query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            GetAuthorByIdQuery query=new GetAuthorByIdQuery(_dbContext, _mapper);
            query.AuthorId = id;
            
            GetAuthorByIdQueryValidator validator=new GetAuthorByIdQueryValidator();
            validator.ValidateAndThrow(query);

            AuthorViewModel result=query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command=new CreateAuthorCommand(_dbContext, _mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command=new UpdateAuthorCommand(_dbContext);
            command.AuthorId= id;
            command.Model= updateAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
            command.AuthorId= id;

            DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

    }
}
