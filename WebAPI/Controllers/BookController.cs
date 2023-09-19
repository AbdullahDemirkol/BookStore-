using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BookOperations.Command.CommandHandler;
using WebAPI.BookOperations.Command.RequestCommandModel;
using WebAPI.BookOperations.Command.Validator;
using WebAPI.BookOperations.Queries.QueriesHandler.GetBook;
using WebAPI.BookOperations.Queries.QueriesHandler.GetBooks;
using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.BookOperations.Queries.Validator;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookById query = new GetBookById(_dbContext,_mapper);
            BookViewModel result;
            try
            {
                query.Id = id;
                GetBookByIdValidator validator = new GetBookByIdValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        // 2 Tane sadece uzantısız HttpGet isteği olunca error verir ve yukarıdaki GetBook methodunu kullanmak daha doğru bir kullanım olacaktır.
        // Bu çözümde alternatif bir çözüm olarak yazılmıştır.
        //[HttpGet]
        //public Book GetBook([FromQuery] string id)
        //{
        //    return bookList.FirstOrDefault(p => p.Id == Convert.ToInt16(id)); ;
        //}


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_dbContext,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator=new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            try
            {
                command.Id = id;
                command.Model = updateBook;
                UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_dbContext);
            try
            {
                command.Id = id;
                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
