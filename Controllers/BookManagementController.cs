using Microsoft.AspNetCore.Mvc;
using App_web_book.Dtos;
using App_web_book.Services;

namespace App_web_book.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookManagementController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Retrieve all books
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            if (books == null || !books.Any())
            {
                return NoContent(); // Return 204 if there are no books
            }
            return Ok(books);
        }

        // Retrieve a specific book by ID
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {bookId} not found." });
            }
            return Ok(book);
        }

        // Add a new book
        [HttpPost("create")]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto bookToAdd)
        {
            if (bookToAdd == null)
            {
                return BadRequest(new { message = "Book data cannot be null." });
            }

            var book = await _bookService.AddBookAsync(bookToAdd);
            return CreatedAtAction(nameof(GetBookById), new { bookId = book.Id }, book);
        }

        // Update an existing book
        [HttpPut("update/{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] CreateBookDto bookUpdates)
        {
            if (bookUpdates == null)
            {
                return BadRequest(new { message = "Book update data cannot be null." });
            }

            var result = await _bookService.UpdateBookAsync(bookId, bookUpdates);
            if (!result)
            {
                return NotFound(new { message = $"Book with ID {bookId} not found." });
            }
            return Ok(new { message = "Book updated successfully." });
        }

        // Delete a book by ID
        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> RemoveBook(int bookId)
        {
            var result = await _bookService.DeleteBookAsync(bookId);
            if (!result)
            {
                return NotFound(new { message = $"Book with ID {bookId} not found." });
            }
            return Ok(new { message = "Book deleted successfully." });
        }
    }
}
