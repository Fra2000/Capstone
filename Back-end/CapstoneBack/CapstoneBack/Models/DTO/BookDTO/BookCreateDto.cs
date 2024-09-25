namespace CapstoneBack.Models.DTO.BookDTO
{
    public class BookCreateDto
    {
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
       
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public List<int> GenreIds { get; set; } 
    }
}
