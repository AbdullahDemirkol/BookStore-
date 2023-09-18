using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;

        public BookController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            return _dbContext.Books.OrderBy(p => p.Id).ToList();
        }

        [HttpGet("{id}")]
        public Book GetBook(string id)
        {
            return _dbContext.Books.FirstOrDefault(p => p.Id == Convert.ToInt16(id)); ;
        }


        // 2 Tane sadece uzantısız HttpGet isteği olunca error verir ve yukarıdaki GetBook methodunu kullanmak daha doğru bir kullanım olacaktır.
        // Bu çözümde alternatif bir çözüm olarak yazılmıştır.
        //[HttpGet]
        //public Book GetBook([FromQuery] string id)
        //{
        //    return bookList.FirstOrDefault(p => p.Id == Convert.ToInt16(id)); ;
        //}


        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Title == newBook.Title);
            if (book is not null || newBook is null)
            {
                return BadRequest();
            }
            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook)
        {
            var book= _dbContext.Books.FirstOrDefault(p=>p.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            book.GenreId=updateBook.GenreId!=default? updateBook.GenreId: book.GenreId;
            book.PageCount=updateBook.PageCount!=default?updateBook.PageCount:book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            _dbContext.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
