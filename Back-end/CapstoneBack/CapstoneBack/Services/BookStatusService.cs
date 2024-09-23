using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.StatusDTO;
using System;

namespace CapstoneBack.Services
{
    public class BookStatusService : IBookStatusService
    {
        private readonly ApplicationDbContext _context;

        public BookStatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo per aggiornare lo stato di un libro e/o il numero di pagine.
        /// </summary>
        public async Task<bool> UpdateBookStatusAsync(int userId, int bookId, string newStatus = null, int? currentPage = null)
        {
            // Recupera lo stato del libro dell'utente
            var userBookStatus = await _context.UserBookStatuses
                .Include(ubs => ubs.Status)
                .FirstOrDefaultAsync(ubs => ubs.UserId == userId && ubs.BookId == bookId);

            if (userBookStatus == null)
            {
                return false;  // Stato del libro non trovato
            }

            // Se `newStatus` è fornito, verifica se è valido
            if (!string.IsNullOrEmpty(newStatus))
            {
                var status = await _context.Statuses.FirstOrDefaultAsync(s => s.StatusName == newStatus);
                if (status == null)
                {
                    return false; // Stato non valido
                }

                // Aggiorna lo stato solo se è diverso dall'attuale
                if (userBookStatus.StatusId != status.StatusId)
                {
                    userBookStatus.StatusId = status.StatusId;
                    userBookStatus.DateUpdated = DateTime.UtcNow;
                }
            }

            // Gestione dello stato "Da Iniziare"
            if (newStatus == "Da Iniziare" && userBookStatus.CurrentPage == null)
            {
                userBookStatus.CurrentPage = 0;
            }

            // Gestione dello stato "In Corso"
            if (newStatus == "In Corso" || userBookStatus.Status.StatusName == "In Corso")
            {
                if (currentPage.HasValue)
                {
                    if (currentPage.Value > userBookStatus.TotalPages || currentPage.Value < 0)
                    {
                        return false;  // Pagina corrente non valida
                    }
                    userBookStatus.CurrentPage = currentPage.Value;
                    userBookStatus.DateUpdated = DateTime.UtcNow;
                }
            }

            // Gestione dello stato "Terminato"
            else if (newStatus == "Terminato")
            {
                // Imposta `currentPage` uguale a `TotalPages` quando il libro è terminato
                userBookStatus.CurrentPage = userBookStatus.TotalPages;
                userBookStatus.DateUpdated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;  // Aggiornamento riuscito
        }

        /// <summary>
        /// Metodo per ottenere tutti i libri con il loro stato per un utente.
        /// </summary>
        public async Task<List<UserBookStatusDto>> GetUserBookStatusesAsync(int userId)
        {
            var userBookStatuses = await _context.UserBookStatuses
                .Where(ubs => ubs.UserId == userId)
                .Include(ubs => ubs.Book)
                .Include(ubs => ubs.Status)
                .ToListAsync();

            if (userBookStatuses == null || userBookStatuses.Count == 0)
            {
                return new List<UserBookStatusDto>();
            }

            return userBookStatuses.Select(ubs => new UserBookStatusDto
            {
                BookId = ubs.BookId,
                BookName = ubs.Book.Name,
                StatusName = ubs.Status.StatusName,
                CurrentPage = ubs.CurrentPage,
                DateUpdated = ubs.DateUpdated,
                CoverImagePath = ubs.Book.CoverImagePath,
                PurchaseDate = ubs.PurchaseDate,
                TotalPages = ubs.TotalPages
            }).ToList();
        }
    }
}
