namespace CapstoneBack.Models.DTO.CartDTO
{
    public class CartReadDto
    {
        public List<CartItemDto> CartItems { get; set; }  
        public decimal CartTotal => CartItems.Sum(item => item.TotalPrice);
    }
}
