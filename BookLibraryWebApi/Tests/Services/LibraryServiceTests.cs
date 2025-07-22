using Xunit;
using Moq;
using BookLibraryWebApi.Application.Services;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using System.Threading.Tasks;
using BookLibraryWebApi.Infrastructure.Repositories;
using AutoMapper;

namespace BookLibraryWebApi.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepoMock = new();
        private readonly Mock<IBorrowRecordRepository> _borrowRepoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _libraryService = new LibraryService(_bookRepoMock.Object, _mapperMock.Object, _borrowRepoMock.Object);
        }

        [Fact]
        public async Task BorrowBookAsync_ShouldReturnNull_IfNotAvailable()
        {
            //arrange
            _bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { Id = 1, IsAvailable = false });
            //act
            var result = await _libraryService.BorrowBookAsync(1, 1);
            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BorrowBookAsync_ShouldReturnNotNull_IfAvailable()
        {
            //arrange
            _bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { Id = 1, IsAvailable = true });
            _bookRepoMock.Setup(r => r.UpdateBookAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);
            _borrowRepoMock.Setup(r => r.AddBorrowRecordAsync(It.IsAny<BorrowRecord>()))
                .Returns(Task.CompletedTask);
            //act
            var result = await _libraryService.BorrowBookAsync(1, 1);
            //assert
            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            _borrowRepoMock.Verify(r => r.AddBorrowRecordAsync(It.IsAny<BorrowRecord>()), Times.Once);
            _bookRepoMock.Verify(r => r.UpdateBookAsync(It.IsAny<Book>()), Times.Once);
        }
        [Fact]
        public async Task ReturnBookAsync_ShouldReturnFalse_IfAvailable()
        {
            //arrange
            _bookRepoMock.Setup(f => f.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync(new Book { IsAvailable = true });
            //act
            var result = await _libraryService.ReturnBookAsync(1, 1);
            //assert 
            Assert.False(result);
        }
        [Fact]
        public async Task ReturnBookAsync_ShouldReturnTrue_IfNotAvilable()
        {
            //arrange
            _bookRepoMock.Setup(r => r.GetBookByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Book { IsAvailable = false });
            _bookRepoMock.Setup(r => r.UpdateBookAsync(It.IsAny<Book>()))
                .Returns(Task.CompletedTask);
            _borrowRepoMock.Setup(r => r.GetActiveBorrowRecordAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new BorrowRecord { ReturnDate = null });
            _borrowRepoMock.Setup(r => r.UpdateBorrowRecordAsync(It.IsAny<BorrowRecord>()))
                .Returns(() => Task.CompletedTask);
            //act
            var result = await _libraryService.ReturnBookAsync(1, 1);
            //assert
            Assert.True(result);
            _bookRepoMock.Verify(r => r.UpdateBookAsync(It.IsAny<Book>()), Times.Once());
            _borrowRepoMock.Verify(f => f.UpdateBorrowRecordAsync(It.IsAny<BorrowRecord>()), Times.Once);
        }
    }
}
