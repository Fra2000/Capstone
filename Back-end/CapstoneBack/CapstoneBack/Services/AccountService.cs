using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CapstoneBack.Models.DTO;

namespace CapstoneBack.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);  

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Role, user.Role.RoleName)  
                }),
                Expires = DateTime.UtcNow.AddHours(2), 
                Audience = "localhost",  
                Issuer = "localhost",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            
            return new AuthResponseDto
            {
                Token = tokenString,
                User = user
            };
        }

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile)
        {
            var hashedPassword = HashPassword(password);

            
            var userRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == "User");
            if (userRole == null)
            {
                throw new Exception("User role not found.");
            }

            
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
                ImagePath = imagePath 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = await _context.Users.Include(u => u.Role)
                            .SingleOrDefaultAsync(u => u.UserId == user.UserId);

            return user;
        }


        
        public async Task<User> RegisterAdminAsync(string firstName, string lastName, string username, string email, string password, IFormFile? imageFile)
        {
            var hashedPassword = HashPassword(password);

            
            var adminRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == "Admin");
            if (adminRole == null)
            {
                throw new Exception("Admin role not found.");
            }

            
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
                ImagePath = imagePath 
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
