using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;

namespace CapstoneBack.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Email == email);

            if (user != null && VerifyPasswordHash(password, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string username, string email, string password, int roleId)
        {
            var hashedPassword = HashPassword(password);

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Email = email,
                PasswordHash = hashedPassword,
                RoleId = roleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users.Include(u => u.Role)
                            .SingleOrDefaultAsync(u => u.UserId == user.UserId);

            return user;
        }

        private bool VerifyPasswordHash(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
