using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneBack.Models;

namespace CapstoneBack.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(int authorId, Author author);
        Task<bool> DeleteAuthorAsync(int authorId);
    }
}
