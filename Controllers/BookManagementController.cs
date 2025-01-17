using Microsoft.AspNetCore.Mvc;
using App_web_book.library;

namespace App_web_book.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BookManagementController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Retrieve all books
        [HttpGet("all")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _dbContext.Books.ToList();
            if (!books.Any())
            {
                return NoContent();
            }
            return Ok(books);
        }

        // Retrieve a specific book by ID
        [HttpGet("{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID {bookId} not found." });
            }
            return Ok(book);
        }

        // Add a new book
        [HttpPost("create")]
        public IActionResult AddBook([FromBody] Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                return BadRequest(new { message = "Book data cannot be null." });
            }

            _dbContext.Books.Add(bookToAdd);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetBookById), new { bookId = bookToAdd.Id }, bookToAdd);
        }

        // Update an existing book
        [HttpPut("update/{bookId}")]
        public IActionResult UpdateBook(int bookId, [FromBody] Book bookDetails)
        {
            var existingBook = _dbContext.Books.Find(bookId);
            if (existingBook == null)
            {
                return NotFound(new { message = $"Book with ID {bookId} not found." });
            }

            existingBook.Name = bookDetails.Name;
            existingBook.Author = bookDetails.Author;
            existingBook.Genre = bookDetails.Genre;
            existingBook.NumberOfPages = bookDetails.NumberOfPages;
            existingBook.PublicationDate = bookDetails.PublicationDate;

            _dbContext.SaveChanges();

            return Ok(new { message = "Book updated successfully." });
        }

        // Delete a book by ID
        [HttpDelete("delete/{bookId}")]
        public IActionResult RemoveBook(int bookId)
        {
            var bookToDelete = _dbContext.Books.Find(bookId);
            if (bookToDelete == null)
            {
                return NotFound(new { message = $"Book with ID {bookId} does not exist." });
            }

            _dbContext.Books.Remove(bookToDelete);
            _dbContext.SaveChanges();

            return Ok(new { message = "Book deleted successfully." });
        }
    }
}
