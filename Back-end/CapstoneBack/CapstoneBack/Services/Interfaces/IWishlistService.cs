using CapstoneBack.Models.DTO.WishlistDTO;

namespace CapstoneBack.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<WishlistDto> AddToWishlistAsync(int userId, WishlistCreateDto wishlistDto);
        Task<IEnumerable<WishlistDto>> GetWishlistByUserIdAsync(int userId);
        Task<WishlistDto> UpdateWishlistItemAsync(int userId, int wishlistId, WishlistUpdateDto updateDto);
        Task<bool> RemoveFromWishlistAsync(int userId, int wishlistId);
        Task<WishlistDto> GetWishlistItemByIdAsync(int wishlistId);
        Task<bool> TransferToCartAsync(int userId, int wishlistId);
    }

}
