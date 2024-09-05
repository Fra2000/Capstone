namespace CapstoneBack.Models.DTO.CartDTO
{
    public class CartItemDto
    {
        public int UserBookId { get; set; }  // Identificativo della voce nel carrello
        public string BookName { get; set; }  // Nome del libro
        public string CoverImagePath { get; set; }  // Percorso dell'immagine di copertina
        public int Quantity { get; set; }  // Quantità di libri nel carrello
        public decimal PricePerUnit { get; set; }  // Prezzo per unità
        public decimal TotalPrice => Quantity * PricePerUnit;
    }

}
