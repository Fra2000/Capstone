using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneBack.Models
{
    public class UserReview
    {
        [Key]
        public int UserReviewId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }  // Valutazione da 1 a 5 stelle

        public string ReviewText { get; set; }  // Testo della recensione

        public DateTime ReviewDate { get; set; }  // Data della recensione
    }

}
