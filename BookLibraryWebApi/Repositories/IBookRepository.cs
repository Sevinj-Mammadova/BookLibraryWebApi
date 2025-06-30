using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryWebApi.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book createdBook);
        Task<Book> UpdateBookAsync(string title, Book updatedBookook);
        Task<Book> DeleteBookByIdAsync(int id);
        Task<Book> DeleteBookByTitleAsync(string title);
        Task<List<Book>> GetFilteredBooksAsync(string? title, string? author, string? genre);
    }
}
