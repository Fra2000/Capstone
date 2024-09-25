using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.AuthorDTO;
using CapstoneBack.Models.DTO.BookDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            return authors.Select(a => new AuthorReadDto
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                DateOfBirth = a.DateOfBirth,
                ImagePath = a.ImagePath,
                Books = a.Books.Select(b => new BookSummaryDto
                {
                    BookId = b.BookId,
                    Name = b.Name
                }).ToList()
            });
        }

        public async Task<AuthorReadDto> GetAuthorByIdAsync(int authorId)
        {
            var author = await _context.Authors
                 .Include(a => a.Books)
                   .ThenInclude(b => b.BookGenres)
                      .ThenInclude(bg => bg.Genre)
                 .SingleOrDefaultAsync(a => a.AuthorId == authorId);

            if (author == null)
            {
                return null;
            }

            
            return new AuthorReadDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                Bio = author.Bio,
                ImagePath = author.ImagePath,
                Books = author.Books.Select(b => new BookSummaryDto
                {
                    BookId = b.BookId,
                    Name = b.Name,
                    Author = new AuthorDto
                    {
                        AuthorId = b.Author.AuthorId,
                        FirstName = b.Author.FirstName,
                        LastName = b.Author.LastName
                    },
                    CoverImagePath = b.CoverImagePath,
                    PublicationDate = b.PublicationDate,
                    Price = b.Price,
                    Genres = b.BookGenres.Select(bg => new GenreDto
                    {
                        GenreId = bg.Genre.GenreId,
                        GenreName = bg.Genre.GenreName
                    }).ToList()
                }).ToList()
            };
        }


        public async Task<Author> CreateAuthorAsync(Author author, IFormFile? imageFile)
        {
            
            string imagePath = "images/Author/default.jpg"; 
            if (imageFile != null && imageFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Author");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imagePath = Path.Combine("images", "Author", fileName);
            }
            author.ImagePath = imagePath;

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(int authorId, Author author, IFormFile? imageFile)
        {
            var existingAuthor = await _context.Authors.FindAsync(authorId);

            if (existingAuthor == null)
            {
                return null;
            }

            
            if (imageFile != null && imageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingAuthor.ImagePath) && !existingAuthor.ImagePath.EndsWith("default.jpg"))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingAuthor.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Author");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                existingAuthor.ImagePath = Path.Combine("images", "Author", fileName);
            }

            
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.DateOfBirth = author.DateOfBirth;
            existingAuthor.Bio = author.Bio;

            await _context.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return false;
            }

            
            if (!string.IsNullOrEmpty(author.ImagePath) && !author.ImagePath.EndsWith("default.jpg"))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", author.ImagePath);
                Console.WriteLine($"File Path: {filePath}"); 

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

       


    }
}
