using CapstoneBack.Models.DTO.CartDTO;

public interface ICartService
{
    Task<UserBookDto> AddOrUpdateCartItemAsync(int userId, UpdateDto cartItemDto);
    Task<List<UserBookDto>> CompletePurchaseAsync(int userId);
    Task<bool> RemoveCartItemAsync(int userBookId, int quantity);  // Aggiungi la quantità da rimuovere
    Task<CartReadDto> GetCartByUserIdAsync(int userId);
}
