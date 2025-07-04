﻿using AutoMapper;
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
        }
    }
}
