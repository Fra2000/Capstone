using CapstoneBack.Models;
using CapstoneBack.Models.DTO.AuthorDTO;


namespace CapstoneBack.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync();
        Task<AuthorReadDto> GetAuthorByIdAsync(int authorId);
        Task<Author> CreateAuthorAsync(Author author, IFormFile? imageFile);
        Task<Author> UpdateAuthorAsync(int authorId, Author author, IFormFile? imageFile);
        Task<bool> DeleteAuthorAsync(int authorId);  
    }
}
