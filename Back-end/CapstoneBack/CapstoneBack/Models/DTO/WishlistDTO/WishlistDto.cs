namespace CapstoneBack.Models.DTO.WishlistDTO
{
    public class WishlistDto
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }  // Nome del libro
        public int Quantity { get; set; }  // Quantità desiderata
        public decimal UnitPrice { get; set; }  // Prezzo per unità del libro
        public decimal TotalPrice { get; set; }  // Prezzo totale basato sulla quantità
    }

}
