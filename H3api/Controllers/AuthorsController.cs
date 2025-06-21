using H3api.Entities;
using H3api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace H3api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetThemAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("first, last")]
        public async Task<ActionResult<List<Author>>> GetMoreAuthors([FromQuery] string first, string last)
        {
            var authors = await _authorRepository.GetAllAsync();

            // Convert authors to a List<Author> to allow Add operation  
            var authorsList = authors.ToList();

            var nyForfatter = new Author
            {
                Id = authors.Last().Id + 1,
                FirstName = first,
                LastName = last
            };
            authorsList.Add(nyForfatter);

            return Ok(authorsList);
        }

        [HttpGet("except")]
        public async Task<ActionResult<IEnumerable<Author>>> GetButAuthors([FromQuery] string except)
        {
            var authors = await _authorRepository.GetAllAsync();
            var filteredAuthors = authors.Where(f => f.FirstName.ToLower() != except);
            return Ok(filteredAuthors);
        }

        [HttpGet("only")]
        public async Task<ActionResult<IEnumerable<Author>>> GetOnlyAuthor([FromQuery] string only)
        {
            var authors = await _authorRepository.GetAllAsync();
            var onlyAuthor = authors.Where(o => o.FirstName.ToLower() == only);
            if (!onlyAuthor.Any())
            {
                return NotFound();
            }
            return Ok(onlyAuthor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            await _authorRepository.AddAsync(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
                return BadRequest();

            if (!await _authorRepository.ExistsAsync(id))
                return NotFound();

            await _authorRepository.UpdateAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            await _authorRepository.DeleteAsync(author);
            return NoContent();
        }

    }
}
