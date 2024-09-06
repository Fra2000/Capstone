using System.ComponentModel.DataAnnotations;

namespace CapstoneBack.Models.DTO.ReviewDTO
{
    public class ReviewCreateDto
    {
        [Required]
        public int BookId { get; set; }  // L'ID del libro recensito.

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }  // Valutazione da 1 a 5.

        [StringLength(500, ErrorMessage = "Review text must not exceed 500 characters.")]
        public string ReviewText { get; set; }  // Testo della recensione.
    }

}
