using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        public static List<Book> bookList = new List<Book>()
        {
            new Book()
            {
                Id=1,
                Title="Lean Startup",
                GenreId="1",
                PageCount=200,
                PublishDate=new DateTime(2001,06,12)
            },
            new Book()
            {
                Id=2,
                Title="Herland",
                GenreId="2",
                PageCount=250,
                PublishDate=new DateTime(2010,05,23)
            },
            new Book()
            {
                Id=3,
                Title="Dune",
                GenreId="2",
                PageCount=540,
                PublishDate=new DateTime(2006,12,21)
            }
        };
        [HttpGet]
        public List<Book> GetBooks()
        {
            return bookList.OrderBy(p => p.Id).ToList();
        }

        [HttpGet("{id}")]
        public Book GetBook(string id)
        {
            return bookList.FirstOrDefault(p => p.Id == Convert.ToInt16(id)); ;
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
            var book = bookList.FirstOrDefault(p => p.Title == newBook.Title);
            if (book is not null || newBook is null)
            {
                return BadRequest();
            }
            bookList.Add(newBook);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook)
        {
            var book=bookList.FirstOrDefault(p=>p.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            book.GenreId=updateBook.GenreId!=default? updateBook.GenreId: book.GenreId;
            book.PageCount=updateBook.PageCount!=default?updateBook.PageCount:book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;

            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = bookList.FirstOrDefault(p => p.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            bookList.Remove(book);
            return Ok();
        }
    }
}
