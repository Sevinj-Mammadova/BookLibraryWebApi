using AutoMapper;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryWebApi.Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public LibraryService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

    }
}
