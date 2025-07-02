using BookLibraryWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryWebApi.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksPaginatedAsync(int pageNumber, int pageSize);
        Task<Book> AddBookAsync(Book createdBook);
        Task<Book> UpdateBookAsync(string title, Book updatedBookook);
        Task<Book> DeleteBookByIdAsync(int id);
        Task<Book> DeleteBookByTitleAsync(string title);
        Task<List<Book>> GetFilteredBooksAsync(string? title, string? author, string? genre);
        Task<List<Book>> SearchBookAsync(string keyword);
    }
}
