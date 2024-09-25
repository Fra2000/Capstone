namespace CapstoneBack.Models.DTO.WishlistDTO
{
    public class WishlistDto
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }  
        public int Quantity { get; set; }  
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice { get; set; }  
    }

}
