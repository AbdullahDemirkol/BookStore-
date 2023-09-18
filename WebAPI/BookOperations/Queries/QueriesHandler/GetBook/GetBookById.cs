using AutoMapper;
using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Queries.QueriesHandler.GetBook
{
    public class GetBookById
    {
        public string Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetBookById(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookViewModel Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Id == Convert.ToInt16(Id));
            if (book == null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı.");
            }
            BookViewModel viewModel = _mapper.Map<BookViewModel>(book);
            return viewModel;
        }
    }
}
