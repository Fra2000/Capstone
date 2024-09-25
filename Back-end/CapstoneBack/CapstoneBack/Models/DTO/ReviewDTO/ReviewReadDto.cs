namespace CapstoneBack.Models.DTO.ReviewDTO
{
    public class ReviewReadDto
    {
        public int UserReviewId { get; set; }
        public int BookId { get; set; }
        public string UserName { get; set; } 
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
    }

}
