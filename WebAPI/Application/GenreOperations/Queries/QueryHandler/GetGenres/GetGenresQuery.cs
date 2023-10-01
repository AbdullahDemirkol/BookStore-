using AutoMapper;
using WebAPI.Application.GenreOperations.Queries.QueryViewModel;
using WebAPI.DataAccess;

namespace WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<GenreViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(p => p.IsActive).OrderBy(p => p.Id);
            List<GenreViewModel> genreViewModels = _mapper.Map<List<GenreViewModel>>(genres);
            return genreViewModels;
        }
    }
}
