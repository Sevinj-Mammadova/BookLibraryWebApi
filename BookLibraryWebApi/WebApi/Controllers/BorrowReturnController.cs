using AutoMapper;
using BookLibraryWebApi.Application.DTOs;
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

        public BorrowReturnController(IBorrowRecordRepository borrowRecordRepository, IMapper mapper)
        {
            _borrowRecordRepository = borrowRecordRepository;
            _mapper = mapper;
        }

        [HttpGet("get-all-borrow-records")]
        public async Task<IActionResult> GetAllBorrowRecords()
        {
            var borrowRecords = await _borrowRecordRepository.GetAllBorrowRecords();
            var borrowRecordDto = _mapper.Map<List<BorrowRecordDto>>(borrowRecords);
            return Ok(borrowRecordDto);
        }
    }
}
