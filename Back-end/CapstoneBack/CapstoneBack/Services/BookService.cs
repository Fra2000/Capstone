using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.BookDTO;
using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllBooksAsync()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .ToListAsync();

             return books.Select(book => new BookReadDto
            {
                BookId = book.BookId,
                Name = book.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                AvailableQuantity = book.AvailableQuantity,
                Author = new AuthorDto
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                },
                Genres = book.BookGenres.Select(bg => new GenreDto
                {
                    GenreId = bg.Genre.GenreId,
                    GenreName = bg.Genre.GenreName
                }).ToList()
            });
        }


        public async Task<BookReadDto> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .SingleOrDefaultAsync(b => b.BookId == bookId);

            if (book == null)
            {
                return null;
            }

            // Mappiamo i dati al DTO
            return new BookReadDto
            {
                BookId = book.BookId,
                Name = book.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                CoverImagePath = book.CoverImagePath,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                AvailableQuantity = book.AvailableQuantity,
                Author = new AuthorDto
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                },
                Genres = book.BookGenres.Select(bg => new GenreDto
                {
                    GenreId = bg.Genre.GenreId,
                    GenreName = bg.Genre.GenreName
                }).ToList()
            };
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


        public async Task<Book> UpdateBookAsync(int bookId, BookUpdateDto bookDto)
        {
            var existingBook = await _context.Books
                .Include(b => b.BookGenres)  // Includi i generi per gestire l'aggiornamento
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (existingBook == null)
            {
                return null;
            }

            // Aggiorna solo i campi modificati
            existingBook.Name = bookDto.Name ?? existingBook.Name;
            existingBook.NumberOfPages = bookDto.NumberOfPages ?? existingBook.NumberOfPages;
            existingBook.Description = bookDto.Description ?? existingBook.Description;

            // Gestione dell'ID dell'autore
            if (bookDto.AuthorId.HasValue)
            {
                var author = await _context.Authors.FindAsync(bookDto.AuthorId.Value);
                if (author != null)
                {
                    existingBook.AuthorId = author.AuthorId;
                }
            }

            existingBook.PublicationDate = bookDto.PublicationDate ?? existingBook.PublicationDate;
            existingBook.Price = bookDto.Price ?? existingBook.Price;

            if (bookDto.AvailableQuantity.HasValue)
            {
                existingBook.AvailableQuantity = bookDto.AvailableQuantity.Value;
            }

            // Gestione dei generi utilizzando solo gli ID
            if (bookDto.GenreIds != null && bookDto.GenreIds.Any())
            {
                // Rimuovi i generi esistenti
                existingBook.BookGenres.Clear();

                // Aggiungi i nuovi generi tramite ID
                foreach (var genreId in bookDto.GenreIds)
                {
                    var genre = await _context.Genres.FindAsync(genreId);
                    if (genre != null)
                    {
                        existingBook.BookGenres.Add(new BookGenre
                        {
                            GenreId = genre.GenreId,
                            BookId = existingBook.BookId
                        });
                    }
                }
            }

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
