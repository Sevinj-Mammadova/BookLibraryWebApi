using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Data;
using BookLibraryWebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BookLibraryWebApi.Tests.Repositories
{
    public class BookRepositoryTests
    {
        private async Task<DataContext> GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            context.Books.AddRange(new List<Book>
            {
                new Book { Id = 1, Author = "A", IsAvailable = true },
                new Book { Id = 2, Author = "B", IsAvailable = true }
            });
            await context.SaveChangesAsync();
            return context;
        }
        [Fact]
        public async Task AddBookAsync_ShouldAddBook()
        {
            //arrange
            var context = await GetInMemoryDbContext();
            var bookRepo = new BookRepository(context);

            //act
            var createdBook = await bookRepo.AddBookAsync(new Book { Id = 3, Author = "C", IsAvailable = true });
            //assert
            Assert.NotNull(createdBook);
            Assert.Equal(3, await context.Books.CountAsync());
            Assert.Equal("C", createdBook.Author);
        }
    }
}
