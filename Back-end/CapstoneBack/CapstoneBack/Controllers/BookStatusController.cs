using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Services.Interfaces;
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

        
        
        [HttpGet("user-statuses")]
        public async Task<IActionResult> GetUserBookStatuses()
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            
            var statuses = await _bookStatusService.GetUserBookStatusesAsync(userId);

            
            if (statuses == null || statuses.Count == 0)
            {
                return NotFound(new { message = "No books found for the user." });
            }

            
            return Ok(statuses);
        }

        
        
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateBookStatus(int bookId, string newStatus = null, int? currentPage = null)
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            
            var result = await _bookStatusService.UpdateBookStatusAsync(userId, bookId, newStatus, currentPage);

           
            if (!result)
            {
                return BadRequest(new { message = "Unable to update the book status or page count." });
            }

            
            return Ok(new { message = "Book status and/or page count updated successfully." });
        }
    }
}
