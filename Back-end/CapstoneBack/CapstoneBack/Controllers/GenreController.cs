using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.GenreDTO;
using CapstoneBack.Models.DTO.BookDTO; // Importa il DTO corretto

namespace CapstoneBack.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreReadDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();

            var genreDtos = genres.Select(g => new GenreReadDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName,
                Books = g.BookGenres?.Any() == true
                    ? g.BookGenres.Select(bg => new BookDto
                    {
                        BookId = bg.BookId,
                        Name = bg.Book.Name
                    }).ToList()
                    : null
            }).ToList();

            return Ok(genreDtos);
        }




        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<GenreReadDto>> CreateGenre([FromBody] GenreCreateDto genreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = new Genre
            {
                GenreName = genreDto.GenreName
            };

            var createdGenre = await _genreService.CreateGenreAsync(genre);

            var genreReadDto = new GenreReadDto
            {
                GenreId = createdGenre.GenreId,
                GenreName = createdGenre.GenreName
                // Notare che non includiamo la lista di libri qui
            };

            return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.GenreId }, genreReadDto);
        }


        // GET: api/Genre/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreReadDto>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            var books = genre.BookGenres?.Select(bg => new BookDto
            {
                BookId = bg.BookId,
                Name = bg.Book.Name
            }).ToList() ?? new List<BookDto>();

            var genreDto = new GenreReadDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Books = books
            };

            return Ok(genreDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreCreateDto genreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGenre = await _genreService.GetGenreByIdAsync(id);
            if (existingGenre == null)
            {
                return NotFound();
            }

            existingGenre.GenreName = genreDto.GenreName;

            var updatedGenre = await _genreService.UpdateGenreAsync(id, existingGenre);

            var genreReadDto = new GenreReadDto
            {
                GenreId = updatedGenre.GenreId,
                GenreName = updatedGenre.GenreName
                
            };

            return Ok(genreReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var existingGenre = await _genreService.GetGenreByIdAsync(id);
            if (existingGenre == null)
            {
                return NotFound();
            }

            var result = await _genreService.DeleteGenreAsync(id);

            if (!result)
            {
                return StatusCode(500, "A problem occurred while deleting the genre.");
            }

            return NoContent();
        }


        


    }
}
