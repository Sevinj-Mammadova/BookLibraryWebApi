using AutoMapper;
using BookLibraryWebApi.Application.DTOs;
using BookLibraryWebApi.Domain.Entities;

namespace BookLibraryWebApi.Application.Mappings
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
            CreateMap<BorrowRecord, BorrowRecordDto>();
            CreateMap<BorrowRecord, BorrowRecordDto>().ReverseMap();
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BorrowRecord, CreateBorrowRecordDto>();
            CreateMap<BorrowRecord, CreateBorrowRecordDto>().ReverseMap();

        }
    }
}
