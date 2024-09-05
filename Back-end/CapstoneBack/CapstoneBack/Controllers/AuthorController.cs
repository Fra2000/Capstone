using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.AuthorDTO; // Assicurati di includere il namespace per il DTO

namespace CapstoneBack.Controllers
{

    [Authorize(Roles = "Admin,SuperAdmin")]
    [ApiController]
    [Route("api/[controller]")]
      // Assicura che solo gli admin possano accedere a queste rotte
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ApplicationDbContext _context;

        public AuthorController(IAuthorService authorService, ApplicationDbContext context)
        {
            _authorService = authorService;
            _context = context;
        }

       

        // GET: api/Author
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            // Mappa ciascun autore in un DTO, senza includere i libri
            var authorDtos = authors.Select(author => new AuthorReadDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                ImagePath = author.ImagePath,
                Books = new List<string>()  // Manteniamo una lista vuota per i libri
            }).ToList();

            return Ok(authorDtos);
        }



        // GET: api/Author/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            // Assicurati che la lista Books non sia null e mappa correttamente i titoli dei libri
            var books = author.Books ?? new List<Book>();

            var authorDto = new AuthorReadDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                Bio = author.Bio,
                ImagePath = author.ImagePath,
                Books = books.Select(b => b.Name).ToList() // Mappa i nomi dei libri
            };

            return Ok(authorDto);
        }




        // POST: api/Author
        [HttpPost]
        public async Task<ActionResult<AuthorReadDto>> CreateAuthor([FromForm] AuthorCreateDto authorDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                DateOfBirth = authorDto.DateOfBirth,
                Bio = authorDto.Bio
            };

            var createdAuthor = await _authorService.CreateAuthorAsync(author, imageFile);

            var authorReadDto = new AuthorReadDto
            {
                AuthorId = createdAuthor.AuthorId,
                FirstName = createdAuthor.FirstName,
                LastName = createdAuthor.LastName,
                DateOfBirth = createdAuthor.DateOfBirth,
                Bio = createdAuthor.Bio,
                ImagePath = createdAuthor.ImagePath,
                Books = new List<string>() // Inizialmente vuoto, poiché l'autore è appena stato creato
            };

            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.AuthorId }, authorReadDto);
        }


        // PUT: api/Author/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromForm] AuthorCreateDto authorDto, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                DateOfBirth = authorDto.DateOfBirth,
                Bio = authorDto.Bio
            };

            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author, imageFile);

            if (updatedAuthor == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                updatedAuthor.AuthorId,
                updatedAuthor.FirstName,
                updatedAuthor.LastName,
                updatedAuthor.DateOfBirth,
                updatedAuthor.Bio,
                updatedAuthor.ImagePath
            });
        }




        // DELETE: api/Author/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }





    }
}
