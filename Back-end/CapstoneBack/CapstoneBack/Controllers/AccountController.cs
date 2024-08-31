using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO;

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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
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
                model.RoleId);

            // Controlla il ruolo e restituisci il DTO appropriato
            if (user.RoleId == 1) // Admin
            {
                var adminDto = new UserAdminDto
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
                    }
                };
                return Ok(new { message = "Registration successful", user = adminDto });
            }
            else // Utente normale
            {
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
                    UserBooks = user.UserBooks,
                    UserBookStatuses = user.UserBookStatuses,
                    UserLoyaltyCards = user.UserLoyaltyCards
                };
                return Ok(new { message = "Registration successful", user = userDto });
            }
        }



        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logout successful" });
        }
    }
}
