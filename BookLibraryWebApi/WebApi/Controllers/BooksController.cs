using AutoMapper;
using BookLibraryWebApi.Application.DTOs;
using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookLibraryWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] PaginationDto pagination)
        {
            var books = await _bookRepository.GetAllBooksPaginatedAsync(pagination.PageNumber, pagination.PageSize);
            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync([FromBody] CreateBookDto createBookDto)
        {
            var createdBook = _mapper.Map<Book>(createBookDto);
            createdBook = await _bookRepository.AddBookAsync(createdBook);
            var createdBookDto = _mapper.Map<CreateBookDto>(createdBook);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBookDto);
        }
        [HttpPut]
        [Route("{title}")]
        public async Task<IActionResult> UpdateBook([FromRoute] string title, [FromBody] UpdateBookDto updateBookDto)
        {
            var updatedBook = _mapper.Map<Book>(updateBookDto);
            updatedBook = await _bookRepository.UpdateBookAsync(title, updatedBook);
            var updatedBookDto = _mapper.Map<UpdateBookDto>(updatedBook);
            return Ok(updatedBookDto);
        }

        [HttpDelete]
        [Route("by-id/{id}")]
        public async Task<IActionResult> DeleteBookByIdAsync([FromRoute] int id)
        {
            var deletedBook = await _bookRepository.DeleteBookByIdAsync(id);
            if (deletedBook == null)
            {
                return NotFound();
            }
            return Ok(deletedBook);
        }
        [HttpDelete]
        [Route("by-title/{title}")]
        public async Task<IActionResult> DeleteBookByTitleAsync(string title)
        {
            var deletedBook = await _bookRepository.DeleteBookByTitleAsync(title);
            if (deletedBook == null)
            {
                return NotFound();
            }
            return Ok(deletedBook);
        }
        [HttpGet("filter")]
        
        public async Task<IActionResult> FilterBooks(string? title,string? author,string? genre)
        {
            var books = await _bookRepository.GetFilteredBooksAsync(title, author, genre);
            return Ok(books);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBookAsync(string keyword)
        {
            var books = await _bookRepository.SearchBookAsync(keyword);
            return Ok(books);
        }
    }
}
