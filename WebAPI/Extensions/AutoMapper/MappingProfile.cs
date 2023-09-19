using AutoMapper;
using WebAPI.BookOperations.Command.RequestCommandModel;
using WebAPI.BookOperations.Queries.QueriesViewModel;
using WebAPI.Entity.Concrete;

namespace WebAPI.Extensions.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.GenreId.ToString()));
            CreateMap<CreateBookModel, Book>();
        }
    }
}
