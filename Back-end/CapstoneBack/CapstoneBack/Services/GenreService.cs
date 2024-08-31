using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;

namespace CapstoneBack.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await _context.Genres
                .Include(g => g.BookGenres)
                    .ThenInclude(bg => bg.Book) // Includiamo i dettagli dei libri associati
                .SingleOrDefaultAsync(g => g.GenreId == genreId);
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> UpdateGenreAsync(int genreId, Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(genreId);

            if (existingGenre == null)
            {
                return null;
            }

            existingGenre.GenreName = genre.GenreName;

            await _context.SaveChangesAsync();
            return existingGenre;
        }

        public async Task<bool> DeleteGenreAsync(int genreId)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre == null)
            {
                return false;
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
