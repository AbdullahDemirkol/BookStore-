using AutoMapper;
using WebAPI.Application.AuthorOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthor
{
    public class GetAuthorByIdQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorViewModel Handle()
        {
            var author=_dbContext.Authors.FirstOrDefault(a=>a.Id== AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }
            AuthorViewModel viewModel=_mapper.Map<AuthorViewModel>(author);
            return viewModel;
        }

    }
}
