using AutoMapper;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.DTOs;

namespace BookLibraryWebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,CreateBookDto>();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
        }
    }
}
