using AutoMapper;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.Application.BookOperations.Commands.CommandHandler
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Title == Model.Title);
            if (book is not null || Model is null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            if (!_dbContext.Genres.Any(p => p.Id == Model.GenreId && p.IsActive == true))
            {
                throw new InvalidOperationException("Böyle bir kitap türü bulunamadı");
            }
            book = _mapper.Map<Book>(Model);
            //book = new Book();
            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = (GenreEnum)Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }
}
