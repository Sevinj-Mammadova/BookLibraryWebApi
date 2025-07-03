using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.EntityFrameworkCore;


namespace BookLibraryWebApi.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Book> AddBookAsync(Book createdBook)
        {
            _dataContext.Books.Add(createdBook);
            await _dataContext.SaveChangesAsync();
            return createdBook;
        }

        public async Task<Book> DeleteBookByIdAsync(int id)
        {
            var deletedBook = await _dataContext.Books.FirstOrDefaultAsync(a => a.Id == id);
            if (deletedBook != null)
            {
                _dataContext.Books.Remove(deletedBook);
                await _dataContext.SaveChangesAsync();
            }
            return deletedBook;
        }

        public async Task<Book> DeleteBookByTitleAsync(string title)
        {
            var deletedBook = await _dataContext.Books.FirstOrDefaultAsync(a => a.Title == title);
            if (deletedBook != null)
            {
                _dataContext.Books.Remove(deletedBook);
                await _dataContext.SaveChangesAsync();
            }
            return deletedBook;
        }

        public async Task<List<Book>> GetFilteredBooksAsync(string? title, string? author, string? genre)
        {
            var query = _dataContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(a => a.Title.ToLower().Equals(title.ToLower()));
            if (!string.IsNullOrEmpty(author))
                query = query.Where(a=> a.Author.ToLower().Equals(author.ToLower()));
            if (!string.IsNullOrEmpty(genre))
                query = query.Where(a=>a.Genre.ToLower().Equals(genre.ToLower()));
            return await query.ToListAsync();
        }

        public async Task<List<Book>> GetAllBooksPaginatedAsync(int pageNumber,int pageSize)
        {
            return await _dataContext.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
             return await _dataContext.Books.FindAsync(id);          
        }

        public async Task<Book> UpdateBookAsync(string title, Book updatedBook)
        {
            var existingBook = await _dataContext.Books.FirstOrDefaultAsync(a => a.Title == title);
            if (existingBook == null)
                return null;
            updatedBook.Id = existingBook.Id;
            _dataContext.Entry(existingBook).CurrentValues.SetValues(updatedBook);
            await _dataContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<List<Book>> SearchBookAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return new List<Book>();
            keyword = keyword.ToLower();
            return await _dataContext.Books
            .Where(b => b.Title.Contains(keyword) ||
                        b.Description.ToLower().Contains(keyword) ||
                       b.Author != null && b.Author.ToLower().Contains(keyword) ||
                       b.Genre != null && b.Genre.ToLower().Contains(keyword)
                       )
                       .ToListAsync();            
        }
        public async Task UpdateBookAsync(Book book)
        {
            _dataContext.Books.Update(book);
            await _dataContext.SaveChangesAsync();
        }
    }
}
