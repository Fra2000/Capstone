using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneBack.Models
{
    public class LoyaltyCardType
    {
        [Key]
        public int LoyaltyCardTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string CardName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinimumSpend { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal DiscountPercentage { get; set; }
        public string AdditionalBenefits { get; set; }
        public ICollection<UserLoyaltyCard> UserLoyaltyCards { get; set; }
    }
}
