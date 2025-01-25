using App_web_book.Dtos;

namespace App_web_book.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task<BookDto> AddBookAsync(CreateBookDto bookToAdd);
        Task<bool> UpdateBookAsync(int bookId, CreateBookDto bookUpdates);
        Task<bool> DeleteBookAsync(int bookId);
    }
}