namespace CapstoneBack.Models.DTO.CartDTO
{
    public class CartItemDto
    {
        public int UserBookId { get; set; }  
        public string BookName { get; set; }  
        public string CoverImagePath { get; set; }  
        public int Quantity { get; set; }  
        public decimal PricePerUnit { get; set; } 
        public decimal TotalPrice => Quantity * PricePerUnit;
    }

}
