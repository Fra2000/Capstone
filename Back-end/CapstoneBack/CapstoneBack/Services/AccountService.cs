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

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile)
        {
            var hashedPassword = HashPassword(password);

            // Trova il ruolo "User"
            var userRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == "User");
            if (userRole == null)
            {
                throw new Exception("User role not found.");
            }

            // Gestisci l'immagine di default o carica l'immagine se fornita
            string imagePath = "images/Account/default.jpg";
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Account");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imagePath = Path.Combine("images", "Account", fileName);
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Email = email,
                PasswordHash = hashedPassword,
                RoleId = userRole.RoleId,
                ImagePath = imagePath // Assegna l'immagine
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users.Include(u => u.Role)
                            .SingleOrDefaultAsync(u => u.UserId == user.UserId);

            return user;
        }


        // Nuovo metodo per registrare un admin
        public async Task<User> RegisterAdminAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile)
        {
            var hashedPassword = HashPassword(password);

            // Trova il ruolo "Admin"
            var adminRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == "Admin");
            if (adminRole == null)
            {
                throw new Exception("Admin role not found.");
            }

            // Gestisci l'immagine di default o carica l'immagine se fornita
            string imagePath = "images/Account/default.jpg";
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Account");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imagePath = Path.Combine("images", "Account", fileName);
            }

            var admin = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = username,
                Email = email,
                PasswordHash = hashedPassword,
                RoleId = adminRole.RoleId,
                ImagePath = imagePath // Assegna l'immagine
            };

            _context.Users.Add(admin);
            await _context.SaveChangesAsync();

            admin = await _context.Users.Include(u => u.Role)
                            .SingleOrDefaultAsync(u => u.UserId == admin.UserId);

            return admin;
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
