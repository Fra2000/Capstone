using CapstoneBack.Models;
using CapstoneBack.Models.DTO;


namespace CapstoneBack.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseDto> AuthenticateAsync(string email, string password);
        Task<User> RegisterUserAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile);
        
       
        Task<User> RegisterAdminAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile);
    }
}
