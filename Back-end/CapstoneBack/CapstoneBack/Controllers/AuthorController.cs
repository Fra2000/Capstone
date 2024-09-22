using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Models;
using CapstoneBack.Services.Interfaces;
using CapstoneBack.Models.DTO.AuthorDTO; 
using CapstoneBack.Models.DTO.BookDTO;
using CapstoneBack.Models.DTO.GenreDTO;

namespace CapstoneBack.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ApplicationDbContext _context;

        public AuthorController(IAuthorService authorService, ApplicationDbContext context)
        {
            _authorService = authorService;
            _context = context;
        }



        
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            
            var authorDtos = authors.Select(author => new AuthorReadDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth,
                ImagePath = author.ImagePath,
                Books = new List<BookSummaryDto>()  
            }).ToList();

            return Ok(authorDtos);
        }



        
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }


            var authorDto = new AuthorReadDto
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
                    Genres = b.Genres.Select(bg => new GenreDto
                    {
                        GenreId = bg.GenreId,
                        GenreName = bg.GenreName
                    }).ToList(),
                }).ToList()
            };

            return Ok(authorDto);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
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
                Books = new List<BookSummaryDto>() // Inizialmente vuoto, poiché l'autore è appena stato creato
            };

            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.AuthorId }, authorReadDto);
        }


        
        [Authorize(Roles = "Admin,SuperAdmin")]
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




        
        [Authorize(Roles = "Admin,SuperAdmin")]
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
