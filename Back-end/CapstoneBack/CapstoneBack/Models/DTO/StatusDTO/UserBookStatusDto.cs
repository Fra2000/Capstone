namespace CapstoneBack.Models.DTO.StatusDTO
{
    public class UserBookStatusDto
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string StatusName { get; set; }
        public int? CurrentPage { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
