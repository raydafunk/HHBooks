using AutoMapper;
using HHBooks.API.Data;
using HHBooks.API.Modles.Author;

namespace HHBooks.API.Configuration
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
             CreateMap<Author,AutorReadOnlyDto>().ReverseMap();
             CreateMap<Author,CreateAuthorDto>().ReverseMap();
              CreateMap<Author,UpdateAurthorDto>().ReverseMap();
                
        }
    }
}
