using Microsoft.AspNetCore.Mvc;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;
using MyProject.Application.Services;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IElasticsearchService _elasticsearchService;

        public BookController(IBookService bookService, IElasticsearchService elasticsearchService)
        {
            _bookService = bookService;
            _elasticsearchService = elasticsearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            var createdBook = await _bookService.AddAsync(bookDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, BookDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();
            await _bookService.UpdateAsync(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexBook([FromBody] BookDto book)
        {
            await _elasticsearchService.IndexDocumentAsync(book);
            return Ok(new { message = "Book indexed successfully" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string query)
        {
            var books = await _elasticsearchService.SearchBooksAsync(query);
            return Ok(books);
        }

        [HttpPost("index-all")]
        public async Task<IActionResult> IndexAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            foreach (var book in books)
            {
                await _elasticsearchService.IndexDocumentAsync(book);
            }

            return Ok(new { message = $"{books.Count()} books indexed successfully" });
        }

        [HttpPost("index-sample")]
        public async Task IndexSampleDataAsync()
        {
            var books = new List<BookDto>
            {
                new BookDto { Id = Guid.NewGuid(), Title = "The Hobbit", Description = "Fantasy novel by J.R.R. Tolkien" },
                new BookDto { Id = Guid.NewGuid(), Title = "Dune", Description = "Science fiction novel by Frank Herbert" }
            };

            foreach (var book in books)
            {
                await _elasticsearchService.IndexDocumentAsync(book);
            }
        }

    }

}
