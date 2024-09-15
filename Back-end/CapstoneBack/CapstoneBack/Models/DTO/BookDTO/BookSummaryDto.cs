using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Models.DTO.BookDTO
{
    public class BookSummaryDto
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public AuthorDto Author { get; set; }
        public string CoverImagePath { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public List<GenreDto> Genres { get; set; }
    }
}

