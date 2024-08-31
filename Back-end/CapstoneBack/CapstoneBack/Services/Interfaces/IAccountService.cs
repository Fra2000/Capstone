using System.Threading.Tasks;
using CapstoneBack.Models;



namespace CapstoneBack.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> RegisterUserAsync(string firstName, string lastName, string username, string email, string password, int roleId);
    }
}
