using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Services.Interfaces;
using System.Security.Claims;
using CapstoneBack.Models.DTO.ReviewDTO;
using Microsoft.AspNetCore.Authorization;


namespace CapstoneBack.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto reviewDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var reviewReadDto = await _reviewService.AddReviewAsync(userId, reviewDto);
            return CreatedAtAction(nameof(GetReview), new { id = reviewReadDto.UserReviewId }, reviewReadDto);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsByBookId(int bookId)
        {
            var reviews = await _reviewService.GetReviewsByBookIdAsync(bookId);
            if (reviews == null)
            {
                return NotFound("No reviews found for this book.");
            }
            return Ok(reviews);
        }

        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
            if (reviews == null)
            {
                return NotFound("No reviews found for this user.");
            }
            return Ok(reviews);
        }

        
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewCreateDto reviewDto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var updatedReview = await _reviewService.UpdateReviewAsync(userId, reviewId, reviewDto);

                return Ok(updatedReview);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result)
            {
                return NotFound("Review not found.");
            }
            return NoContent();
        }
    }
}
