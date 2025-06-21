using H3api.Dtos;
using H3api.Entities;
using H3api.Interfaces;
using H3api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace H3api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoversController : ControllerBase
{
    private readonly ICoverRepository _coverRepo;
    private readonly IArtistRepository _artistRepo;

    public CoversController(ICoverRepository coverRepo, IArtistRepository artistRepo, IBookRepository bookRepo)
    {
        _coverRepo = coverRepo;
        _artistRepo = artistRepo;
    }

    // GET: api/covers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoverDto>>> GetAll()
    {
        var covers = await _coverRepo.GetAllAsync();

        var result = covers.Select(c => new CoverDto
        {
            CoverId = c.CoverId,
            DesignIdeas = c.DesignIdeas,
            DigitalOnly = c.DigitalOnly,
            BookId = c.BookId,
            ArtistNames = c.Artists.Select(a => $"{a.FirstName} {a.LastName}").ToList()
        });

        return Ok(result);
    }

    // GET: api/covers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CoverDto>> GetById(int id)
    {
        var cover = await _coverRepo.GetByIdAsync(id);
        if (cover == null) return NotFound();

        var dto = new CoverDto
        {
            CoverId = cover.CoverId,
            DesignIdeas = cover.DesignIdeas,
            DigitalOnly = cover.DigitalOnly,
            BookId = cover.BookId,
            ArtistNames = cover.Artists.Select(a => $"{a.FirstName} {a.LastName}").ToList()
        };

        return Ok(dto);
    }

    // POST: api/covers
    [HttpPost]
    public async Task<ActionResult<CoverDto>> Create([FromBody] CreateCoverDto dto)
    {
        var cover = new Cover
        {
            DesignIdeas = dto.DesignIdeas,
            DigitalOnly = dto.DigitalOnly,
            BookId = dto.BookId,
            Artists = new List<Artist>()
        };

        foreach (var artistId in dto.ArtistIds)
        {
            var artist = await _artistRepo.GetByIdAsync(artistId);
            if (artist != null)
            {
                cover.Artists.Add(artist);
            }
        }

















        await _coverRepo.AddAsync(cover);
        var created = await _coverRepo.GetByIdAsync(cover.CoverId);

        var result = new CoverDto
        {
            CoverId = created!.CoverId,
            DesignIdeas = created.DesignIdeas,
            DigitalOnly = created.DigitalOnly,
            BookId = created.BookId,
            ArtistNames = created.Artists.Select(a => $"{a.FirstName} {a.LastName}").ToList()
        };

        return Ok(result);
    }

    // PUT: api/covers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCoverDto dto)
    {
        if (id != dto.CoverId) return BadRequest();

        var cover = await _coverRepo.GetByIdAsync(id);
        if (cover == null) return NotFound();

        cover.DesignIdeas = dto.DesignIdeas;
        cover.DigitalOnly = dto.DigitalOnly;
        cover.BookId = dto.BookId;

        // Opdater Artists
        cover.Artists.Clear();
        foreach (var artistId in dto.ArtistIds)
        {
            var artist = await _artistRepo.GetByIdAsync(artistId);
            if (artist != null)
            {
                cover.Artists.Add(artist);
            }
        }

        await _coverRepo.UpdateAsync(cover);
        return NoContent();
    }

    // DELETE: api/covers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cover = await _coverRepo.GetByIdAsync(id);
        if (cover == null) return NotFound();

        await _coverRepo.DeleteAsync(cover);
        return NoContent();
    }
}
