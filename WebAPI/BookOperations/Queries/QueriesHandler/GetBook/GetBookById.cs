using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Queries.QueriesHandler.GetBook
{
    public class GetBookById
    {
        public string Id { get; set; }
        private readonly BookStoreDbContext _dbContext;


        public GetBookById(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookViewModel Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == Convert.ToInt16(Id));
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı.");
            }
            BookViewModel viewModel = new BookViewModel()
            {
                Title = book.Title,
                PageCount = book.PageCount,
                Genre = book.GenreId.ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            };
            return viewModel;
        }
    }
}
