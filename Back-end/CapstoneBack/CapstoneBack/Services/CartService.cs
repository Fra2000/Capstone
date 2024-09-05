using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
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

        // Recupera il carrello di un utente
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

        // Aggiunge o aggiorna un libro nel carrello
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

        // Rimuove un libro dal carrello
        public async Task<bool> RemoveCartItemAsync(int userBookId, int quantity)
        {
            var userBook = await _context.UserBooks.FindAsync(userBookId);
            if (userBook == null || quantity <= 0)
            {
                return false;  // Elemento non trovato o quantità non valida
            }

            // Se la quantità da rimuovere è maggiore o uguale alla quantità attuale, rimuovi l'elemento
            if (quantity >= userBook.Quantity)
            {
                _context.UserBooks.Remove(userBook);
            }
            else
            {
                // Riduci la quantità
                userBook.Quantity -= quantity;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        // Completa l'acquisto e restituisce i libri acquistati dall'utente
        public async Task<List<UserBookDto>> CompletePurchaseAsync(int userId)
        {
            // Recupera i libri nel carrello dell'utente
            var userBooks = await _context.UserBooks
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Book)
                .ThenInclude(b => b.Author)  // Includi l'autore del libro
                .ToListAsync();

            var userBookDtos = new List<UserBookDto>();

            // Aggiungi i libri allo stato "Da Iniziare"
            foreach (var userBook in userBooks)
            {
                var userBookStatus = new UserBookStatus
                {
                    UserId = userId,
                    BookId = userBook.BookId,
                    StatusId = 1,  // Assumendo che "Da Iniziare" sia lo status con ID 1
                    CurrentPage = 0,  // Inizialmente a pagina 0
                    TotalPages = userBook.Book.NumberOfPages,  // Imposta il numero di pagine totale dal libro
                    PurchaseDate = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                _context.UserBookStatuses.Add(userBookStatus);

                // Prepara il DTO per restituire i dati all'utente
                userBookDtos.Add(new UserBookDto
                {
                    BookId = userBook.BookId,
                    Name = userBook.Book.Name,
                    AuthorName = userBook.Book.Author.FullName,
                    CoverImagePath = userBook.Book.CoverImagePath
                });
            }

            // Rimuovi i libri dal carrello dopo l'acquisto
            _context.UserBooks.RemoveRange(userBooks);

            // Salva i cambiamenti nel database
            await _context.SaveChangesAsync();

            return userBookDtos;
        }





    }
}
