using AutoMapper;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryWebApi.Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IBorrowRecordRepository _borrowRecordRepository;

        public LibraryService(IBookRepository bookRepository, IMapper mapper, IBorrowRecordRepository borrowRecordRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _borrowRecordRepository = borrowRecordRepository;
        }
        public async Task<BorrowRecord?> BorrowBookAsync(int bookId, int userId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null || !book.IsAvailable)
                return null;
            book.IsAvailable = false;
            await _bookRepository.UpdateBookAsync(book);
            var newBorrow = new BorrowRecord
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                ReturnDate = null
            };
            await _borrowRecordRepository.AddBorrowRecordAsync(newBorrow);
            return newBorrow;
        }

        public async Task<bool> ReturnBookAsync(int bookId, int userId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if(book == null || book.IsAvailable)
                return false;
            book.IsAvailable = true;
            await _bookRepository.UpdateBookAsync(book);
            var returnedBook = await _borrowRecordRepository.GetActiveBorrowRecordAsync(bookId, userId);
            if(returnedBook == null)
                return false;
            returnedBook.ReturnDate = DateTime.Now.ToLocalTime();
            await _borrowRecordRepository.UpdateBorrowRecordAsync(returnedBook);
            return true;
        }

    }
}