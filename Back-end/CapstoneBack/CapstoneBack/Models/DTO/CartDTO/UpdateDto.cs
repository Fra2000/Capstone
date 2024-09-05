namespace CapstoneBack.Models.DTO.CartDTO
{
    public class UpdateDto
    {
        public int BookId { get; set; }  // Identificativo del libro da aggiungere o aggiornare
        public int Quantity { get; set; }  // Quantità di libri da aggiungere o aggiornare
    }
}
