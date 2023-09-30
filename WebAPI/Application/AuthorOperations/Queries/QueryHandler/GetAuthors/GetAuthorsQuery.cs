using AutoMapper;
using WebAPI.Application.AuthorOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<AuthorViewModel> Handle()
        {
            var authors = _dbContext.Authors.OrderBy(a => a.Id).ToList();
            List<AuthorViewModel> viewModels=_mapper.Map<List<AuthorViewModel>>(authors);
            return viewModels;
        }


    }
}
