using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenres;
using WebAPI.Application.GenreOperations.Commands.CommandHandler;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.Application.GenreOperations.Commands.Validator;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using WebAPI.Application.GenreOperations.Queries.QueryViewModel;
using WebAPI.Application.GenreOperations.Queries.Validator;
using WebAPI.DataAccess;
using FluentValidation;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery getGenresQuery = new GetGenresQuery(_dbContext, _mapper);

            var result = getGenresQuery.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            query.GenreId = id;

            GetGenreByIdQueryValidator validator=new GetGenreByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody]CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody]UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.GenreId= id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator=new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.GenreId = id;

            DeleteGenreCommandValidator validator= new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok(command);
        }

    }
}
