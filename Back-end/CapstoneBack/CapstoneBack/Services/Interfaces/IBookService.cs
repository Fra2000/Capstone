using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneBack.Models;

namespace CapstoneBack.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> UpdateBookAsync(int bookId, Book book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
