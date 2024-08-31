﻿namespace CapstoneBack.Models.DTO
{
    public class UserDto : UserAdminDto
    {
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<UserBookStatus> UserBookStatuses { get; set; }
        public ICollection<UserLoyaltyCard> UserLoyaltyCards { get; set; }
    }
}
