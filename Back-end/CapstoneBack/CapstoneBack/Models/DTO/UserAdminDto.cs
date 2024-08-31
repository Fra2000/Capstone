using CapstoneBack.Models.DTO;

namespace CapstoneBack.Models.DTO
{
    public class UserAdminDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleDto Role { get; set; }
    }
}

