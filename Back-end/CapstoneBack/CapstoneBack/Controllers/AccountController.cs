using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.LoginRegister;

namespace CapstoneBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("check-role")]
        public IActionResult CheckRole()
        {
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            return Ok(new { roles });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _accountService.AuthenticateAsync(model.Email, model.Password);
            if (user == null)
                return Unauthorized(new { message = "Email or password is incorrect" });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(new { message = "Login successful" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model, IFormFile? imageFile)
        {
            var userExists = await _accountService.AuthenticateAsync(model.Email, model.Password);
            if (userExists != null)
                return BadRequest(new { message = "User already exists" });

            var user = await _accountService.RegisterUserAsync(
                model.FirstName,
                model.LastName,
                model.Username,
                model.Email,
                model.Password,
                imageFile);

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = new RoleDto
                {
                    RoleId = user.Role.RoleId,
                    RoleName = user.Role.RoleName
                },
                ImagePath = user.ImagePath, // Aggiungi l'immagine nel DTO
                UserBooks = user.UserBooks,
                UserBookStatuses = user.UserBookStatuses,
                UserLoyaltyCards = user.UserLoyaltyCards
            };

            return Ok(new { message = "Registration successful", user = userDto });
        }


        // Nuovo endpoint per registrare un admin
        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegisterModel model, IFormFile? imageFile)
        {
            var userExists = await _accountService.AuthenticateAsync(model.Email, model.Password);
            if (userExists != null)
                return BadRequest(new { message = "Admin user already exists" });

            var admin = await _accountService.RegisterAdminAsync(
                model.FirstName,
                model.LastName,
                model.Username,
                model.Email,
                model.Password,
                imageFile);

            var adminDto = new UserAdminDto
            {
                UserId = admin.UserId,
                UserName = admin.UserName,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                PasswordHash = admin.PasswordHash,
                Role = new RoleDto
                {
                    RoleId = admin.Role.RoleId,
                    RoleName = admin.Role.RoleName
                },
                ImagePath = admin.ImagePath // Aggiungi l'immagine nel DTO
            };

            return Ok(new { message = "Admin registration successful", user = adminDto });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Esegui il logout dell'utente autenticato
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Puoi anche cancellare i cookie di autenticazione se necessario
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

            return Ok(new { message = "Logout successful" });
        }
    }
}
