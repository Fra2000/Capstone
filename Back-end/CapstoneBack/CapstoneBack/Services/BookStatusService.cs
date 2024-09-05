using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.StatusDTO;

namespace CapstoneBack.Services
{
    public class BookStatusService : IBookStatusService
    {
        private readonly ApplicationDbContext _context;

        public BookStatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Metodo per aggiornare lo stato di un libro e/o il numero di pagine
        public async Task<bool> UpdateBookStatusAsync(int userId, int bookId, string newStatus = null, int? currentPage = null)
        {
            // Recupera lo stato del libro dell'utente
            var userBookStatus = await _context.UserBookStatuses
                .Include(ubs => ubs.Status)  // Includi lo stato corrente del libro
                .FirstOrDefaultAsync(ubs => ubs.UserId == userId && ubs.BookId == bookId);

            if (userBookStatus == null)
            {
                return false;  // Stato del libro non trovato
            }

            bool isStatusUpdated = false;
            string currentStatusName = userBookStatus.Status.StatusName;

            // Se viene fornito un nuovo stato, aggiorna lo stato
            if (!string.IsNullOrEmpty(newStatus))
            {
                var status = await _context.Statuses.FirstOrDefaultAsync(s => s.StatusName == newStatus);
                if (status == null)
                {
                    return false;  // Stato non valido
                }

                // Aggiorna lo stato solo se è diverso dall'attuale
                if (userBookStatus.StatusId != status.StatusId)
                {
                    userBookStatus.StatusId = status.StatusId;
                    userBookStatus.DateUpdated = DateTime.UtcNow;  // Aggiorna la data solo se cambia lo stato
                    isStatusUpdated = true;  // Stato aggiornato
                }
            }

            // Permetti di aggiornare il numero di pagine solo se lo stato era "In Corso" e rimane "In Corso"
            if (currentStatusName == "In Corso" && (userBookStatus.Status.StatusName == "In Corso") && currentPage.HasValue)
            {
                // Verifica che la pagina corrente non superi il numero totale di pagine
                if (currentPage.Value > userBookStatus.TotalPages)
                {
                    return false;  // La pagina corrente non può superare il totale delle pagine del libro
                }

                userBookStatus.CurrentPage = currentPage.Value;
                userBookStatus.DateUpdated = DateTime.UtcNow;  // Aggiorna la data anche se cambia il numero di pagine
            }

            await _context.SaveChangesAsync();
            return true;  // Aggiornamento riuscito
        }




        // Metodo per ottenere tutti i libri con il loro stato per un utente
        public async Task<List<UserBookStatusDto>> GetUserBookStatusesAsync(int userId)
        {
            return await _context.UserBookStatuses
                .Where(ubs => ubs.UserId == userId)
                .Include(ubs => ubs.Book)
                .Include(ubs => ubs.Status)
                .Select(ubs => new UserBookStatusDto
                {
                    BookId = ubs.BookId,
                    BookName = ubs.Book.Name,
                    StatusName = ubs.Status.StatusName,
                    CurrentPage = ubs.CurrentPage,
                    DateUpdated = ubs.DateUpdated
                })
                .ToListAsync();
        }
    }
}
