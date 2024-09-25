using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Models.DTO.CartDTO;

namespace CapstoneBack.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<CartReadDto> GetCartByUserIdAsync(int userId)
        {
            var cartItems = await _context.UserBooks
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Book)
                .Select(ub => new CartItemDto
                {
                    UserBookId = ub.UserBookId,
                    BookName = ub.Book.Name,
                    CoverImagePath = ub.Book.CoverImagePath,
                    Quantity = ub.Quantity,
                    PricePerUnit = ub.Book.Price
                })
                .ToListAsync();

            return new CartReadDto
            {
                CartItems = cartItems
            };
        }

        
        public async Task<UserBookDto> AddOrUpdateCartItemAsync(int userId, UpdateDto cartItemDto)
        {
            var existingUserBook = await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == cartItemDto.BookId);

            if (existingUserBook != null)
            {
                existingUserBook.Quantity += cartItemDto.Quantity;
            }
            else
            {
                var userBook = new UserBook
                {
                    UserId = userId,
                    BookId = cartItemDto.BookId,
                    Quantity = cartItemDto.Quantity,
                    PurchaseDate = DateTime.UtcNow
                };

                _context.UserBooks.Add(userBook);
            }

            await _context.SaveChangesAsync();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == cartItemDto.BookId);

            return new UserBookDto
            {
                BookId = book.BookId,
                Name = book.Name,
                AuthorName = book.Author.FullName,
                CoverImagePath = book.CoverImagePath
            };
        }

        
        public async Task<bool> RemoveCartItemAsync(int userBookId, int quantity)
        {
            var userBook = await _context.UserBooks.FindAsync(userBookId);
            if (userBook == null || quantity <= 0)
            {
                return false;  
            }

            
            if (quantity >= userBook.Quantity)
            {
                _context.UserBooks.Remove(userBook);
            }
            else
            {
                
                userBook.Quantity -= quantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }


       
        public async Task<List<UserBookDto>> CompletePurchaseAsync(int userId)
        {
            
            var userBooks = await _context.UserBooks
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Book)
                .ThenInclude(b => b.Author)  
                .ToListAsync();

            var userBookDtos = new List<UserBookDto>();

            
            foreach (var userBook in userBooks)
            {
                var userBookStatus = new UserBookStatus
                {
                    UserId = userId,
                    BookId = userBook.BookId,
                    StatusId = 1,  
                    CurrentPage = 0,  
                    TotalPages = userBook.Book.NumberOfPages,  
                    PurchaseDate = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                _context.UserBookStatuses.Add(userBookStatus);

                
                userBookDtos.Add(new UserBookDto
                {
                    BookId = userBook.BookId,
                    Name = userBook.Book.Name,
                    AuthorName = userBook.Book.Author.FullName,
                    CoverImagePath = userBook.Book.CoverImagePath
                });
            }

            
            _context.UserBooks.RemoveRange(userBooks);

            
            await _context.SaveChangesAsync();

            return userBookDtos;
        }





    }
}
