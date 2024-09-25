using CapstoneBack.Models;
using CapstoneBack.Models.DTO.BookDTO;

namespace CapstoneBack.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookReadDto>> GetAllBooksAsync();
        Task<BookReadDto> GetBookByIdAsync(int bookId);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(int bookId, BookUpdateDto book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
