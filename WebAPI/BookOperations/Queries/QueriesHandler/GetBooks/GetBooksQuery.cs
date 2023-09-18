using AutoMapper;
using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.Common;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Queries.QueriesHandler.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(p => p.Id).ToList();
            List<BookViewModel> viewModel = _mapper.Map<List<BookViewModel>>(bookList);
            return viewModel;
        }
    }
}
