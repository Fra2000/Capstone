using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.BookDTO;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDbContext _context;
        

        public BookController(IBookService bookService, ApplicationDbContext context)
        {
            _bookService = bookService;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSummaryDto>>> GetAllBooks(
        [FromQuery] int? genreId,
        [FromQuery] int? authorId,
        [FromQuery] string orderBy = "Name",
        [FromQuery] bool ascending = true)
        {

            var booksQuery = _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                .AsQueryable();

            if (genreId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.BookGenres.Any(bg => bg.GenreId == genreId.Value));
            }

            if (authorId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.AuthorId == authorId.Value);
            }

        
            booksQuery = orderBy switch
            {
                "PublicationDate" => ascending
                    ? booksQuery
                        .OrderBy(b => b.PublicationDate.Year)
                        .ThenBy(b => b.PublicationDate.Month)
                        .ThenBy(b => b.PublicationDate.Day)
                    : booksQuery
                        .OrderByDescending(b => b.PublicationDate.Year)
                        .ThenByDescending(b => b.PublicationDate.Month)
                        .ThenByDescending(b => b.PublicationDate.Day),
                "Price" => ascending
                    ? booksQuery.OrderBy(b => b.Price)
                    : booksQuery.OrderByDescending(b => b.Price),
                    _ => ascending
                    ? booksQuery.OrderBy(b => b.Name)
                    : booksQuery.OrderByDescending(b => b.Name),
            };



            var books = await booksQuery.ToListAsync();

            var bookSummaryDtos = books.Select(book => new BookSummaryDto
            {
                BookId = book.BookId,
                Name = book.Name,
                Author = new AuthorDto
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                },
                CoverImagePath = book.CoverImagePath,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                Genres = book.BookGenres.Select(bg => new GenreDto
                {
                    GenreId = bg.Genre.GenreId,
                    GenreName = bg.Genre.GenreName
                }).ToList()
            }).ToList();

            return Ok(bookSummaryDtos);
        }






        
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<BookReadDto>> CreateBook([FromForm] BookCreateDto bookDto, IFormFile? coverImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imagePath = null;

            if (coverImage != null && coverImage.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Book");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(coverImage.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }
                imagePath = Path.Combine("images", "Book", fileName);
            }
            else
            {
                
                imagePath = Path.Combine("images", "Book", "default.png");
            }

            var book = new Book
            {
                Name = bookDto.Name,
                NumberOfPages = bookDto.NumberOfPages,
                Description = bookDto.Description,
                AuthorId = bookDto.AuthorId,
                PublicationDate = bookDto.PublicationDate,
                Price = bookDto.Price,
                AvailableQuantity = bookDto.AvailableQuantity,
                CoverImagePath = imagePath,
                BookGenres = bookDto.GenreIds.Select(id => new BookGenre
                {
                    GenreId = id
                }).ToList()
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

           
            book = await _context.Books
                .Include(b => b.Author)  
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)  
                .FirstOrDefaultAsync(b => b.BookId == book.BookId);

            var bookReadDto = new BookReadDto
            {
                BookId = book.BookId,
                Name = book.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                Author = new AuthorDto
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                },
                CoverImagePath = book.CoverImagePath,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                AvailableQuantity = book.AvailableQuantity,
                Genres = book.BookGenres.Select(bg => new GenreDto
                {
                    GenreId = bg.Genre.GenreId,
                    GenreName = bg.Genre.GenreName
                }).ToList()
            };

            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, bookReadDto);
        }






       
        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound(); 
            }

            var bookReadDto = new BookReadDto
            {
                BookId = book.BookId,
                Name = book.Name,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                Author = new AuthorDto
                {
                    AuthorId = book.Author.AuthorId,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                },
                CoverImagePath = book.CoverImagePath,
                PublicationDate = book.PublicationDate,
                Price = book.Price,
                AvailableQuantity = book.AvailableQuantity,
                Genres = book.Genres.Select(bg => new GenreDto
                {
                    GenreId = bg.GenreId,
                    GenreName = bg.GenreName
                }).ToList()
            };

            return Ok(bookReadDto);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromForm] BookUpdateDto bookDto, IFormFile? coverImage)
        {
            var existingBook = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (existingBook == null)
            {
                return NotFound();
            }

            
            existingBook.Name = bookDto.Name ?? existingBook.Name;
            existingBook.NumberOfPages = bookDto.NumberOfPages ?? existingBook.NumberOfPages;
            existingBook.Description = bookDto.Description ?? existingBook.Description;

           
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

           
            if (coverImage != null && coverImage.Length > 0)
            {
                
                if (!string.IsNullOrEmpty(existingBook.CoverImagePath) && !existingBook.CoverImagePath.EndsWith("default.png"))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.CoverImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

               
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Book");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(coverImage.FileName);
                var newFilePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(stream);
                }

                
                existingBook.CoverImagePath = Path.Combine("images", "Book", fileName);
            }

            
            if (bookDto.GenreIds != null && bookDto.GenreIds.Any())
            {
                existingBook.BookGenres.Clear();

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

           
            var bookReadDto = new BookReadDto
            {
                BookId = existingBook.BookId,
                Name = existingBook.Name,
                NumberOfPages = existingBook.NumberOfPages,
                Description = existingBook.Description,
                Author = new AuthorDto
                {
                    AuthorId = existingBook.Author.AuthorId,
                    FirstName = existingBook.Author.FirstName,
                    LastName = existingBook.Author.LastName
                },
                CoverImagePath = existingBook.CoverImagePath,  
                PublicationDate = existingBook.PublicationDate,
                Price = existingBook.Price,
                AvailableQuantity = existingBook.AvailableQuantity,
                Genres = existingBook.BookGenres.Select(bg => new GenreDto
                {
                    GenreId = bg.Genre.GenreId,
                    GenreName = bg.Genre.GenreName
                }).ToList()
            };

            return Ok(bookReadDto);
        }







        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            
            if (!string.IsNullOrEmpty(book.CoverImagePath) && !book.CoverImagePath.EndsWith("default.png"))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.CoverImagePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }




    }
}
