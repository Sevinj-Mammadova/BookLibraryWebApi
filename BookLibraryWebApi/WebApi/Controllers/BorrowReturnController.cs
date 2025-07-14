using AutoMapper;
using BookLibraryWebApi.Application.DTOs;
using BookLibraryWebApi.Application.Interfaces;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryWebApi.WebApi.Controllers
{
    [Route("api/Borrow-Records")]
    [ApiController]
    public class BorrowReturnController : ControllerBase
    {
        private readonly IBorrowRecordRepository _borrowRecordRepository;
        private readonly IMapper _mapper;
        private readonly ILibraryService _libraryService;

        public BorrowReturnController(IBorrowRecordRepository borrowRecordRepository, IMapper mapper, ILibraryService libraryService)
        {
            _borrowRecordRepository = borrowRecordRepository;
            _mapper = mapper;
            _libraryService = libraryService;
        }

        [HttpGet("get-all-borrow-records")]
        public async Task<IActionResult> GetAllBorrowRecords()
        {
            var borrowRecords = await _borrowRecordRepository.GetAllBorrowRecordsAsync();
            var borrowRecordDto = _mapper.Map<List<BorrowRecordDto>>(borrowRecords);
            return Ok(borrowRecordDto);
        }
        [HttpPost("add-borrow_records")]
        public async Task<IActionResult> AddBorrowRecords([FromBody] CreateBorrowRecordDto createBorrowRecordDto)
        {
            var createdBorrowRecord = await _libraryService.BorrowBookAsync(createBorrowRecordDto.BookId, createBorrowRecordDto.UserId);
            if (createdBorrowRecord == null)
                return BadRequest("Borrowing failed. Book may be unavailable.");
            var createdBorrowRecordDto = new BorrowRecordDto
            {
                Id = createdBorrowRecord.Id,
                BookId = createdBorrowRecord.BookId,
                UserId = createdBorrowRecord.UserId,
                BorrowDate = createdBorrowRecord.BorrowDate.ToLocalTime(),
                DueDate = createdBorrowRecord.DueDate.ToLocalTime(),
                ReturnDate = createdBorrowRecord.ReturnDate?.ToLocalTime(),
            };

            return Ok(createdBorrowRecordDto);
        }
        [HttpPatch("return-borrowed-book")]
        public async Task<IActionResult> ReturnBorrowedBook([FromBody] ReturnBookRequest returnBookRequest) 
        {
            var success = await _libraryService.ReturnBookAsync(returnBookRequest.BookId, returnBookRequest.UserId);
            if(success == false)
            {
                return BadRequest("Unable to return the book.");
            }
            return Ok("Returned successfully.");
        }
        [HttpGet("user-history")]
        public async Task<IActionResult> GetUserHistory(int userId)
        {
            var borrowRecords = await _borrowRecordRepository.GetBorrowRecordsByUserIdAsync(userId);
            if (!borrowRecords.Any())
            {
                return NotFound("No borrow history found for this user.");
            }
            return Ok(borrowRecords);
        }
    }
}
