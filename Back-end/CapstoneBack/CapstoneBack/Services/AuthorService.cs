using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using Microsoft.AspNetCore.Http;
using CapstoneBack.Services.Interfaces;
using System.IO;

namespace CapstoneBack.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await _context.Authors
                .Include(a => a.Books) // Include i libri associati all'autore
                .SingleOrDefaultAsync(a => a.AuthorId == authorId);
        }


        public async Task<Author> CreateAuthorAsync(Author author, IFormFile? imageFile)
        {
            // Gestisci l'immagine di default o carica l'immagine se fornita
            string imagePath = "images/Author/default.jpg"; // Percorso dell'immagine di default
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

            // Gestisci l'immagine di default o carica l'immagine se fornita
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

            // Aggiorna le altre proprietà dell'autore
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

            // Gestisci la cancellazione del file immagine, evitando di eliminare l'immagine di default
            if (!string.IsNullOrEmpty(author.ImagePath) && !author.ImagePath.EndsWith("default.jpg"))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", author.ImagePath);
                Console.WriteLine($"File Path: {filePath}"); // Debug del percorso

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
