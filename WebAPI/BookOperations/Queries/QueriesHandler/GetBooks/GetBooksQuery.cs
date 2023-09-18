using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.Common;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Queries.QueriesHandler.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(p => p.Id).ToList();
            List<BookViewModel> viewModel = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                viewModel.Add(new BookViewModel
                {
                    Title = book.Title,
                    PageCount = book.PageCount,
                    Genre = book.GenreId.ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
                });
            }
            return viewModel;
        }
    }
}
