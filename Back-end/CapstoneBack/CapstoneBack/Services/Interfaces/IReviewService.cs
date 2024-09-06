using CapstoneBack.Models.DTO.ReviewDTO;

namespace CapstoneBack.Services.Interfaces
{
    public interface IReviewService
    {
        // Aggiunge una nuova recensione
        Task<ReviewReadDto> AddReviewAsync(int userId, ReviewCreateDto reviewDto);

        // Recupera tutte le recensioni di un libro
        Task<IEnumerable<ReviewReadDto>> GetReviewsByBookIdAsync(int bookId);

        // Recupera tutte le recensioni fatte da un utente
        Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(int userId);
        Task<ReviewReadDto> UpdateReviewAsync(int userId, int reviewId, ReviewCreateDto reviewDto);

        // (Opzionale) Cancella una recensione specifica
        Task<bool> DeleteReviewAsync(int reviewId);

        Task<ReviewReadDto> GetReviewByIdAsync(int reviewId);
    }

}
