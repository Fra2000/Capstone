using Microsoft.EntityFrameworkCore;
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

       
        public async Task<bool> UpdateBookStatusAsync(int userId, int bookId, string newStatus = null, int? currentPage = null)
        {
            
            var userBookStatus = await _context.UserBookStatuses
                .Include(ubs => ubs.Status)
                .FirstOrDefaultAsync(ubs => ubs.UserId == userId && ubs.BookId == bookId);

            if (userBookStatus == null)
            {
                return false;  
            }

            
            if (!string.IsNullOrEmpty(newStatus))
            {
                var status = await _context.Statuses.FirstOrDefaultAsync(s => s.StatusName == newStatus);
                if (status == null)
                {
                    return false; 
                }

                
                if (userBookStatus.StatusId != status.StatusId)
                {
                    userBookStatus.StatusId = status.StatusId;
                    userBookStatus.DateUpdated = DateTime.UtcNow;
                }
            }

            
            if (newStatus == "Da Iniziare" && userBookStatus.CurrentPage == null)
            {
                userBookStatus.CurrentPage = 0;
            }

            
            if (newStatus == "In Corso" || userBookStatus.Status.StatusName == "In Corso")
            {
                if (currentPage.HasValue)
                {
                    if (currentPage.Value > userBookStatus.TotalPages || currentPage.Value < 0)
                    {
                        return false;  
                    }
                    userBookStatus.CurrentPage = currentPage.Value;
                    userBookStatus.DateUpdated = DateTime.UtcNow;
                }
            }

            
            else if (newStatus == "Terminato")
            {
                
                userBookStatus.CurrentPage = userBookStatus.TotalPages;
                userBookStatus.DateUpdated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;  
        }

        
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
