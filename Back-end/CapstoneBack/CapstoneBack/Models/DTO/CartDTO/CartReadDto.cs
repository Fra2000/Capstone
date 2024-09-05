namespace CapstoneBack.Models.DTO.CartDTO
{
    public class CartReadDto
    {
        public List<CartItemDto> CartItems { get; set; }  // Elenco dei libri nel carrello
        public decimal CartTotal => CartItems.Sum(item => item.TotalPrice);
    }
}
