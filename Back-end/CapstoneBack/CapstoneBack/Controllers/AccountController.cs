using System.Security.Claims;
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
            var authResponse = await _accountService.AuthenticateAsync(model.Email, model.Password);
            if (authResponse == null)
                return Unauthorized(new { message = "Email or password is incorrect" });

            return Ok(new
            {
                token = authResponse.Token,
                user = new
                {
                    authResponse.User.UserId,
                    authResponse.User.Email,
                    authResponse.User.FirstName,
                    authResponse.User.LastName,
                    Role = authResponse.User.Role.RoleName
                }
            });   
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
                ImagePath = user.ImagePath,
                UserBooks = user.UserBooks,
                UserBookStatuses = user.UserBookStatuses,
                UserLoyaltyCards = user.UserLoyaltyCards
            };

            return Ok(new { message = "Registration successful", user = userDto });
        }


       
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
                ImagePath = admin.ImagePath
            };

            return Ok(new { message = "Admin registration successful", user = adminDto });
        }
        
    }
}
