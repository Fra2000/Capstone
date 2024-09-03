namespace CapstoneBack.Models.DTO.LoginRegister
{
    public class UserDto : UserAdminDto
    {
        public string ImagePath { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<UserBookStatus> UserBookStatuses { get; set; }
        public ICollection<UserLoyaltyCard> UserLoyaltyCards { get; set; }
    }
}
