namespace CapstoneBack.Models.DTO.BookDTO
{
    public class BookReadDto
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string CoverImagePath { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public List<string> Genres { get; set; } // Nomi dei generi associati
    }
}
