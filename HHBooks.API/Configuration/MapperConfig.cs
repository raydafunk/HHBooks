using AutoMapper;
using HHBooks.API.Data;
using HHBooks.API.Modles.Author;
using HHBooks.API.Modles.Book;

namespace HHBooks.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Author, AutorReadOnlyDto>().ReverseMap();
            CreateMap<Author, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, UpdateAurthorDto>().ReverseMap();

            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q => q.AutorName, d => d.MapFrom( map => $"{map.Author!.FirstName} {map.Author.LastName}"))
                .ReverseMap();
            CreateMap<Book, BookDetailsDto>()
                .ForMember(q => q.AutorName, d => d.MapFrom(map => $"{map.Author!.FirstName} {map.Author.LastName}"))
                .ReverseMap();



        }
    }
}
