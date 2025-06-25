using BookLibraryWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryWebApi.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
    }
}
