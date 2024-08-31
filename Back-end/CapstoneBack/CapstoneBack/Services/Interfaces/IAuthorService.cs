using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneBack.Models;
using Microsoft.AspNetCore.Http;

namespace CapstoneBack.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<Author> CreateAuthorAsync(Author author, IFormFile? imageFile);
        Task<Author> UpdateAuthorAsync(int authorId, Author author, IFormFile? imageFile);
        Task<bool> DeleteAuthorAsync(int authorId);  // Metodo per eliminare un autore
    }
}
