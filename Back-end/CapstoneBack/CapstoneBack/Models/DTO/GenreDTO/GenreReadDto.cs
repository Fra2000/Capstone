using CapstoneBack.Models.DTO.BookDTO;

namespace CapstoneBack.Models.DTO.GenreDTO
{
    public class GenreReadDto
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public List<BookDto> Books { get; set; }
    }
}
