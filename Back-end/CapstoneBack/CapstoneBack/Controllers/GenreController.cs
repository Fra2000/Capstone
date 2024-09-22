using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.GenreDTO;
using CapstoneBack.Models.DTO.BookDTO; 
using CapstoneBack.Models.DTO.AuthorDTO;

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

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreReadDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();

            var genreDtos = genres.Select(g => new GenreReadDto
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName,
                Books = g.BookGenres?.Any() == true
                    ? g.BookGenres.Select(bg => new BookSummaryDto
                    {
                        BookId = bg.BookId,
                        Name = bg.Book.Name
                    }).ToList()
                    : null
            }).ToList();

            return Ok(genreDtos);
        }




        
        [Authorize(Roles = "Admin,SuperAdmin")]
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
               
            };

            return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.GenreId }, genreReadDto);
        }


        
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreReadDto>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            var books = genre.BookGenres?.Select(bg => new BookSummaryDto
            {
                BookId = bg.BookId,
                Name = bg.Book.Name,
                Author = new AuthorDto
                {
                    AuthorId = bg.Book.Author.AuthorId,
                    FirstName = bg.Book.Author.FirstName,
                    LastName = bg.Book.Author.LastName
                },
                CoverImagePath = bg.Book.CoverImagePath,
                PublicationDate = bg.Book.PublicationDate,
                Price = bg.Book.Price,
                Genres = bg.Book.BookGenres.Select(g => new GenreDto
                {
                    GenreId = g.Genre.GenreId,
                    GenreName = g.Genre.GenreName
                }).ToList()
            }).ToList() ?? new List<BookSummaryDto>();

            var genreDto = new GenreReadDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
                Books = books
            };

            return Ok(genreDto);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
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
        [Authorize(Roles = "Admin,SuperAdmin")]
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
