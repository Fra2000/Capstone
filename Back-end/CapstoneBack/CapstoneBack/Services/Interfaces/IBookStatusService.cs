using System.Threading.Tasks;
using System.Collections.Generic;
using CapstoneBack.Models.DTO.StatusDTO;  // Creeremo questo DTO successivamente

namespace CapstoneBack.Services.Interfaces
{
    public interface IBookStatusService
    {
        // Metodo per aggiornare lo stato di un libro
        Task<bool> UpdateBookStatusAsync(int userId, int bookId, string newStatus, int? currentPage = null);

        // Metodo per ottenere tutti i libri con il loro stato per un utente
        Task<List<UserBookStatusDto>> GetUserBookStatusesAsync(int userId);
    }
}
