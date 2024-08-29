using CapstoneBack.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBack.Models
{
    public class UserLoyaltyCard
    {
        [Key]
        public int UserLoyaltyCardId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("LoyaltyCardType")]
        public int LoyaltyCardTypeId { get; set; }
        public LoyaltyCardType LoyaltyCardType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalSpent { get; set; } = 0;
        public DateTime CardAssignedDate { get; set; }
    }
}
