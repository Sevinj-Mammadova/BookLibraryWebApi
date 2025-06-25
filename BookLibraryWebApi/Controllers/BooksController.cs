using BookLibraryWebApi.Data;
using BookLibraryWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();           
            return Ok(books);
        }
    }
}
