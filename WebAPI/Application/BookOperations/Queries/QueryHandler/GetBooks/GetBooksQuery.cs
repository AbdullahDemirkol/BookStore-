using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.BookOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.BookOperations.Queries.QueryHandler.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(p => p.Author).Include(p => p.Genre).OrderBy(p => p.Id).ToList();
            List<BookViewModel> viewModels = _mapper.Map<List<BookViewModel>>(bookList);
            return viewModels;
        }
    }
}
