using System.ComponentModel.DataAnnotations;

namespace CapstoneBack.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(100)]
        public string GenreName { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
