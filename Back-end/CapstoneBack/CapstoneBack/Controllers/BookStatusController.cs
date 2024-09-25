using CapstoneBack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var statuses = await _bookStatusService.GetUserBookStatusesAsync(userId);

            if (statuses == null || statuses.Count == 0)
            {
                return NotFound(new { message = "No books found for the user." });
            }

            return Ok(statuses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while retrieving the book statuses: {ex.Message}" });
        }
    }

    
    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateBookStatus(int bookId, string? newStatus = null, int? currentPage = null)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

           
            if (string.IsNullOrEmpty(newStatus) && currentPage == null)
            {
                return BadRequest(new { message = "At least one of 'newStatus' or 'currentPage' must be provided." });
            }

            var result = await _bookStatusService.UpdateBookStatusAsync(userId, bookId, newStatus, currentPage);

            if (!result)
            {
                return BadRequest(new { message = "Unable to update the book status or page count. Ensure that the status and page count are valid." });
            }

            return Ok(new { message = "Book status and/or page count updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"An error occurred while updating the book status: {ex.Message}" });
        }
    }
}
