namespace CapstoneBack.Models.DTO.BookDTO
{
    public class BookSummaryDto
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string CoverImagePath { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public List<string> Genres { get; set; }
    }
}

