using AutoMapper;
using WebAPI.Application.AuthorOperations.Commands.RequestCommandModel;
using WebAPI.Application.AuthorOperations.Queries.QueryViewModel;
using WebAPI.Application.BookOperations.Commands.RequestCommandModel;
using WebAPI.Application.BookOperations.Queries.QueryViewModel;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.Application.GenreOperations.Queries.QueryViewModel;
using WebAPI.Application.UserOperations.Commands.RequestCommandModel;
using WebAPI.Entity.Concrete;

namespace WebAPI.Extensions.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(destinationMember: dest=>dest.Author,memberOptions: opt=>opt.MapFrom(mapExpression: src=>src.Author.Name+" "+src.Author.Surname));
            CreateMap<CreateBookModel, Book>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Author, AuthorViewModel>().ForMember(dest=>dest.FullName,opt=>opt.MapFrom(src=>src.Name+" "+src.Surname));
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<CreateUserModel, User>().ForMember(dest=>dest.RefreshToken,opt=>opt.MapFrom(src=>""));
        }
    }
}
