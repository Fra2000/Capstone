using CapstoneBack.Models;

namespace CapstoneBack.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task<Genre> UpdateGenreAsync(int genreId, Genre genre);
        Task<bool> DeleteGenreAsync(int genreId);
    }
}
