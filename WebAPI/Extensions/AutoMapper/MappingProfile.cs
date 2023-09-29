using AutoMapper;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Queries.QueryViewModel;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.Application.GenreOperations.Queries.QueryViewModel;
using WebAPI.Entity.Concrete;

namespace WebAPI.Extensions.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<CreateBookModel, Book>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<CreateGenreModel, Genre>();
        }
    }
}
