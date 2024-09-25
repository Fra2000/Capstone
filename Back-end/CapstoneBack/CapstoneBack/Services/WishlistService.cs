using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.WishlistDTO;

namespace CapstoneBack.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WishlistDto> AddToWishlistAsync(int userId, WishlistCreateDto wishlistDto)
        {
            var book = await _context.Books.FindAsync(wishlistDto.BookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            var wishlist = new Wishlist
            {
                UserId = userId,
                BookId = wishlistDto.BookId,
                Quantity = wishlistDto.Quantity
            };

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return new WishlistDto
            {
                WishlistId = wishlist.WishlistId,
                BookId = book.BookId,
                BookName = book.Name,
                Quantity = wishlist.Quantity,
                UnitPrice = book.Price,
                TotalPrice = book.Price * wishlist.Quantity
            };
        }

        public async Task<IEnumerable<WishlistDto>> GetWishlistByUserIdAsync(int userId)
        {
            return await _context.Wishlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Book)
                .Select(w => new WishlistDto
                {
                    WishlistId = w.WishlistId,
                    BookId = w.BookId,
                    BookName = w.Book.Name,
                    Quantity = w.Quantity,
                    UnitPrice = w.Book.Price,
                    TotalPrice = w.Book.Price * w.Quantity
                })
                .ToListAsync();
        }

        public async Task<WishlistDto> GetWishlistItemByIdAsync(int wishlistId)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.Book)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId);

            if (wishlist == null)
            {
                return null;  
            }

            return new WishlistDto
            {
                WishlistId = wishlist.WishlistId,
                BookId = wishlist.BookId,
                BookName = wishlist.Book.Name,
                Quantity = wishlist.Quantity,
                UnitPrice = wishlist.Book.Price,
                TotalPrice = wishlist.Book.Price * wishlist.Quantity
            };
        }


        public async Task<WishlistDto> UpdateWishlistItemAsync(int userId, int wishlistId, WishlistUpdateDto updateDto)
        {
            var wishlist = await _context.Wishlists
                .Include(w => w.Book)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlist == null)
                throw new InvalidOperationException("Wishlist item not found.");

            wishlist.Quantity = updateDto.Quantity;
            await _context.SaveChangesAsync();

            return new WishlistDto
            {
                WishlistId = wishlist.WishlistId,
                BookId = wishlist.Book.BookId,
                BookName = wishlist.Book.Name,
                Quantity = wishlist.Quantity,
                UnitPrice = wishlist.Book.Price,
                TotalPrice = wishlist.Book.Price * wishlist.Quantity
            };
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int wishlistId)
        {
            var wishlist = await _context.Wishlists.FindAsync(wishlistId);
            if (wishlist == null || wishlist.UserId != userId)
                return false;

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferToCartAsync(int userId, int wishlistId)
        {
            var wishlistItem = await _context.Wishlists
                .Include(w => w.Book)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                return false; 
            }

           
            var cartItem = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == wishlistItem.BookId);

            if (cartItem != null)
            {
                
                cartItem.Quantity += wishlistItem.Quantity;
            }
            else
            {
                
                var newUserBook = new UserBook
                {
                    UserId = userId,
                    BookId = wishlistItem.BookId,
                    Quantity = wishlistItem.Quantity,
                    PurchaseDate = DateTime.UtcNow
                };
                _context.UserBooks.Add(newUserBook);
            }

            
            _context.Wishlists.Remove(wishlistItem);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
