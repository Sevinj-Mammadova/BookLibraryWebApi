using Xunit;
using Moq;
using BookLibraryWebApi.Application.Services;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using System.Threading.Tasks;
using BookLibraryWebApi.Infrastructure.Repositories;

namespace BookLibraryWebApi.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepoMock = new();
        private readonly Mock<IBorrowRecordRepository> _borrowRepoMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();

        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _libraryService = new LibraryService(
                _bookRepoMock.Object,
                _mapperMock.Object,
                _borrowRepoMock.Object
            );
        }

        [Fact]
        public async Task BorrowBookAsync_ShouldReturnNull_IfBookNotAvailable()
        {
            _bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { IsAvailable = false });

            var result = await _libraryService.BorrowBookAsync(1, 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task BorrowBookAsync_ShouldCreateBorrowRecord_IfBookAvailable()
        {
            var book = new Book { Id = 1, IsAvailable = true };

            _bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync(book);
            _bookRepoMock.Setup(r => r.UpdateBookAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);
            _borrowRepoMock.Setup(r => r.AddBorrowRecordAsync(It.IsAny<BorrowRecord>())).Returns(Task.CompletedTask);

            var result = await _libraryService.BorrowBookAsync(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            _bookRepoMock.Verify(r => r.UpdateBookAsync(It.Is<Book>(b => !b.IsAvailable)), Times.Once);
            _borrowRepoMock.Verify(r => r.AddBorrowRecordAsync(It.IsAny<BorrowRecord>()), Times.Once);
        }
    }
}
