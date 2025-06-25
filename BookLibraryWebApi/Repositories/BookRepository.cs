using BookLibraryWebApi.Data;
using BookLibraryWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookLibraryWebApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _dataContext.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
             return await _dataContext.Books.FindAsync(id);          
        }

    }
}
