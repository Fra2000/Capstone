using CapstoneBack.Models.DTO.ReviewDTO;

namespace CapstoneBack.Services.Interfaces
{
    public interface IReviewService
    {
        
        Task<ReviewReadDto> AddReviewAsync(int userId, ReviewCreateDto reviewDto);

        
        Task<IEnumerable<ReviewReadDto>> GetReviewsByBookIdAsync(int bookId);

        
        Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(int userId);
        Task<ReviewReadDto> UpdateReviewAsync(int userId, int reviewId, ReviewCreateDto reviewDto);

       
        Task<bool> DeleteReviewAsync(int reviewId);

        Task<ReviewReadDto> GetReviewByIdAsync(int reviewId);
    }

}
