using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.BookOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.BookOperations.Queries.QueryHandler.GetBook
{
    public class GetBookByIdQuery
    {
        public int Id { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;


        public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookViewModel Handle()
        {
            var book = _dbContext.Books.Include(p=>p.Author).Include(p=>p.Genre).FirstOrDefault(p => p.Id == Id);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            BookViewModel viewModel = _mapper.Map<BookViewModel>(book);
            return viewModel;
        }
    }
}
