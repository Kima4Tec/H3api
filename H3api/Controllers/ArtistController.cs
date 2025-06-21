using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using H3api.Interfaces;
using H3api.Entities;
using H3api.Repositories;

namespace H3api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;
        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetAllAsync()
        {
            var artists = await _artistRepository.GetAllAsync();
            return Ok(artists);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Artist>> GetByIdAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }
        /// <summary>
        /// Retrieves one artist by first name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        [HttpGet("firstName")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetOnlyArtist([FromQuery] string firstName)
        {
            var artist = await _artistRepository.GetAllAsync();
            var onlyArtist = artist.Where(o => o.FirstName == firstName);
            if (!onlyArtist.Any())
            {
                return NotFound();
            }
            return Ok(onlyArtist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> CreateAsync([FromBody] Artist artist)
        {
            if (artist == null)
            {
                return BadRequest("Artist cannot be null.");
            }
            await _artistRepository.AddAsync(artist);

            var createdArtist = await _artistRepository.GetByIdAsync(artist.ArtistId);
            return Ok(createdArtist);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Artist artist)
        {
            if (artist == null)
            {
                return BadRequest("Artist cannot be null.");
            }
            await _artistRepository.UpdateAsync(artist);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            await _artistRepository.DeleteAsync(artist);
            return NoContent();
        }
    }
}
