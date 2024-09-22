using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models.DTO.CartDTO;
using System.Security.Claims;

namespace CapstoneBack.Controllers
{

    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

       
        [HttpGet]
        public async Task<ActionResult<CartReadDto>> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

      
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCartItem([FromBody] UpdateDto cartItemDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var cartItem = await _cartService.AddOrUpdateCartItemAsync(userId, cartItemDto);
            return Ok(cartItem);
        }

        
        [HttpDelete("{userBookId}")]
        public async Task<IActionResult> RemoveCartItem(int userBookId, [FromQuery] int quantity = 1)
        {
            
            var result = await _cartService.RemoveCartItemAsync(userBookId, quantity);

            if (!result)
            {
                return BadRequest(new { message = "Unable to update the quantity or invalid quantity provided." });
            }

            return NoContent();  
        }


       
        [HttpPost("complete-purchase")]
        public async Task<IActionResult> CompletePurchase()
        {
            
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

           
            var userBooks = await _cartService.CompletePurchaseAsync(userId);

            
            if (userBooks == null || !userBooks.Any())
            {
                return NotFound(new { message = "No books found or purchase failed." });
            }

            return Ok(userBooks);
        }

    }
}
