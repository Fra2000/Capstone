using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneBack.Models;
using Microsoft.AspNetCore.Http;
using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.BookDTO;

namespace CapstoneBack.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync();
        Task<AuthorReadDto> GetAuthorByIdAsync(int authorId);
        Task<Author> CreateAuthorAsync(Author author, IFormFile? imageFile);
        Task<Author> UpdateAuthorAsync(int authorId, Author author, IFormFile? imageFile);
        Task<bool> DeleteAuthorAsync(int authorId);  // Metodo per eliminare un autore
    }
}
