using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;

namespace CapstoneBack.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();
        }


        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .SingleOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Carica l'autore e i generi dopo aver salvato il libro
            book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .SingleOrDefaultAsync(b => b.BookId == book.BookId);

            return book;
        }


        public async Task<Book> UpdateBookAsync(int bookId, Book book)
        {
            var existingBook = await _context.Books.FindAsync(bookId);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Name = book.Name;
            existingBook.NumberOfPages = book.NumberOfPages;
            existingBook.Description = book.Description;
            existingBook.AuthorId = book.AuthorId;
            existingBook.CoverImagePath = book.CoverImagePath;
            existingBook.PublicationDate = book.PublicationDate;
            existingBook.Price = book.Price;
            existingBook.AvailableQuantity = book.AvailableQuantity;


            // Gestire l'aggiornamento delle relazioni con i generi
            existingBook.BookGenres = book.BookGenres;

            await _context.SaveChangesAsync();
            return existingBook;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
