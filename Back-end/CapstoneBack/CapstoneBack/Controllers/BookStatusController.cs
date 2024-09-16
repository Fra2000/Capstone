using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Services.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CapstoneBack.Controllers
{

    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class BookStatusController : ControllerBase
    {
        private readonly IBookStatusService _bookStatusService;

        public BookStatusController(IBookStatusService bookStatusService)
        {
            _bookStatusService = bookStatusService;
        }

        // Ottieni tutti i libri con il loro stato per l'utente autenticato
        
        [HttpGet("user-statuses")]
        public async Task<IActionResult> GetUserBookStatuses()
        {
            // Ottieni l'ID dell'utente autenticato dal token o dai cookie
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Recupera gli status dei libri dell'utente
            var statuses = await _bookStatusService.GetUserBookStatusesAsync(userId);

            // Se non ci sono libri con status
            if (statuses == null || statuses.Count == 0)
            {
                return NotFound(new { message = "No books found for the user." });
            }

            // Restituisci la lista degli status
            return Ok(statuses);
        }

        // Aggiorna lo status di un libro per l'utente autenticato
        
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateBookStatus(int bookId, string newStatus = null, int? currentPage = null)
        {
            // Ottieni l'ID dell'utente autenticato dal token o dai cookie
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Aggiorna lo stato del libro e/o il numero di pagine se lo stato è "In Corso"
            var result = await _bookStatusService.UpdateBookStatusAsync(userId, bookId, newStatus, currentPage);

            // Verifica se l'aggiornamento è avvenuto con successo
            if (!result)
            {
                return BadRequest(new { message = "Unable to update the book status or page count." });
            }

            // Restituisci un messaggio di successo
            return Ok(new { message = "Book status and/or page count updated successfully." });
        }
    }
}
