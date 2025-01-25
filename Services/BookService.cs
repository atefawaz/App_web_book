using App_web_book.Data.VSDC;
using App_web_book.Dtos;
using App_web_book.Entities.VSDCEntities;
using Microsoft.EntityFrameworkCore;

namespace App_web_book.Services
{
    public class BookService : IBookService
    {
        private readonly BookdbContext _context;

        public BookService(BookdbContext context)
        {
            _context = context;
        }

        // Retrieve all books
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _context.Books
                .Select(book => new BookDto
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author
                })
                .ToListAsync();
        }

        // Retrieve a specific book by ID
        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author
            };
        }

        // Add a new book
        public async Task<BookDto> AddBookAsync(CreateBookDto bookToAdd)
        {
            var book = new Book
            {
                Name = bookToAdd.Name,
                Author = bookToAdd.Author,
                Genre = bookToAdd.Genre,
                NumberOfPages = bookToAdd.NumberOfPages,
                PublicationDate = bookToAdd.PublicationDate
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author
            };
        }

        // Update an existing book
        public async Task<bool> UpdateBookAsync(int bookId, CreateBookDto bookUpdates)
        {
            var existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null) return false;

            existingBook.Name = bookUpdates.Name;
            existingBook.Author = bookUpdates.Author;
            existingBook.Genre = bookUpdates.Genre;
            existingBook.NumberOfPages = bookUpdates.NumberOfPages;
            existingBook.PublicationDate = bookUpdates.PublicationDate;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a book by ID
        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
