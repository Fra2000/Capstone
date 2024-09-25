using CapstoneBack.Models.DTO.StatusDTO;  

namespace CapstoneBack.Services.Interfaces
{
    public interface IBookStatusService
    {        
        Task<bool> UpdateBookStatusAsync(int userId, int bookId, string newStatus, int? currentPage = null);

        Task<List<UserBookStatusDto>> GetUserBookStatusesAsync(int userId);
    }
}
