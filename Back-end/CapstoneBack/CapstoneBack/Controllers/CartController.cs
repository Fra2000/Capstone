using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CapstoneBack.Services.Interfaces;
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

        // Ottieni il carrello dell'utente autenticato
        [HttpGet]
        public async Task<ActionResult<CartReadDto>> GetCart()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        // Aggiungi o aggiorna un libro nel carrello
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCartItem([FromBody] UpdateDto cartItemDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var cartItem = await _cartService.AddOrUpdateCartItemAsync(userId, cartItemDto);
            return Ok(cartItem);
        }

        // Rimuovi un libro dal carrello
        // Rimuovi una quantità specifica di un libro dal carrello
        [HttpDelete("{userBookId}")]
        public async Task<IActionResult> RemoveCartItem(int userBookId, [FromQuery] int quantity = 1)
        {
            // Chiama il service per rimuovere una quantità specifica dal carrello
            var result = await _cartService.RemoveCartItemAsync(userBookId, quantity);

            if (!result)
            {
                return BadRequest(new { message = "Unable to update the quantity or invalid quantity provided." });
            }

            return NoContent();  // Operazione completata con successo
        }


        // Completa l'acquisto
        [HttpPost("complete-purchase")]
        public async Task<IActionResult> CompletePurchase()
        {
            // Ottieni l'ID dell'utente autenticato
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Completa l'acquisto e salva i libri acquistati con lo status "Da Iniziare"
            var userBooks = await _cartService.CompletePurchaseAsync(userId);

            // Se nessun libro è stato trovato o c'è stato un errore
            if (userBooks == null || !userBooks.Any())
            {
                return NotFound(new { message = "No books found or purchase failed." });
            }

            // Restituisci la lista dei libri acquistati senza il prezzo
            return Ok(userBooks);
        }

    }
}
