using AutoMapper;
using BookLibraryWebApi.Application.DTOs;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Repositories;
using BookLibraryWebApi.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookLibraryWebApi.Tests.Controllers
{
    public class BorrowReturnControllerTests
    {
        private readonly Mock<IBorrowRecordRepository> borrowRecordRepoMock = new();
        private readonly Mock<ILibraryService> libraryServiceMock = new();
        private readonly Mock<IMapper> mapperMock = new();
        private BorrowReturnController _controller;

        public BorrowReturnControllerTests()
        {
            _controller = new BorrowReturnController(borrowRecordRepoMock.Object, mapperMock.Object, libraryServiceMock.Object);
        }

        [Fact]
        public async Task AddBorrowRecords_ShouldReturnOk_WhenBorrowSuccessful()
        {
            //arrange
            var createdBorrowRecord = new BorrowRecord
            {
                Id = 3,
                BookId = 5,
                UserId = 6,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };
            libraryServiceMock.Setup(r => r.BorrowBookAsync(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(createdBorrowRecord);
            var createBorrowRecordDto = new CreateBorrowRecordDto { BookId = 5, UserId = 6 };
            //act
            var result = await _controller.AddBorrowRecords(createBorrowRecordDto);
            //assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdItem = Assert.IsAssignableFrom<BorrowRecordDto>(okResult.Value);
            Assert.Equal(createdBorrowRecord.Id, createdItem.Id);
        }
    }
}
