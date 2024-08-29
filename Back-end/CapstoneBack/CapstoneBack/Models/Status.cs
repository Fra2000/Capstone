using System.ComponentModel.DataAnnotations;

namespace CapstoneBack.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }

        public ICollection<UserBookStatus> UserBookStatuses { get; set; }
    }
}
