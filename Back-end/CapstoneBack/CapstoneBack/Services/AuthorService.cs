using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;

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
            return await _context.Authors.SingleOrDefaultAsync(a => a.AuthorId == authorId);
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAuthorAsync(int authorId, Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(authorId);

            if (existingAuthor == null)
            {
                return null;
            }

            // Aggiorna le proprietà dell'autore esistente
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.DateOfBirth = author.DateOfBirth;
            existingAuthor.Bio = author.Bio;
            existingAuthor.ImagePath = author.ImagePath;

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

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
