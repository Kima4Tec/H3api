using H3api.Dtos;
using H3api.Entities;
using H3api.Interfaces;
using H3api.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepo;
    private readonly IAuthorRepository _authorRepository;

    public BooksController(IBookRepository bookRepo, IAuthorRepository authorRepository)
    {
        _bookRepo = bookRepo;
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _bookRepo.GetAllAsync();
        var bookDtos = books.Select(b => new BookDto
        {
            BookId = b.BookId,
            Title = b.Title,
            PublishYear = b.PublishDate.Year,
            Price = b.Price,
            AuthorFullName = $"{b.Author.FirstName} {b.Author.LastName}",
            CoverDesignIdeas = b.Cover?.DesignIdeas,
            ArtistFullNames = b.Cover?.Artists?.Select(a => $"{a.FirstName} {a.LastName}").ToList()
        }).ToList();

        return Ok(bookDtos);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookRepo.GetByIdWithDetailsAsync(id);

        if (book == null)
            return NotFound();

        var dto = new BookDto
        {
            BookId = book.BookId,
            Title = book.Title,
            PublishYear = book.PublishDate.Year,
            Price = book.Price,
            AuthorFullName = $"{book.Author.FirstName} {book.Author.LastName}",
            CoverDesignIdeas = book.Cover?.DesignIdeas,
            ArtistFullNames = book.Cover?.Artists?.Select(a => $"{a.FirstName} {a.LastName}").ToList()
        };

        return Ok(dto);
    }



    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(CreateBookDto dto)
    {
        var author = await _authorRepository.GetByIdAsync(dto.AuthorId);
        if (author == null)
        {
            return BadRequest("Author not found.");
        }

        var book = new Book
        {
            Title = dto.Title,
            PublishDate = new DateOnly(dto.PublishYear, 1, 1),
            Price = dto.Price,
            AuthorId = dto.AuthorId
        };

        await _bookRepo.AddAsync(book);
        var created = await _bookRepo.GetByIdAsync(book.BookId);

        var result = new BookDto
        {
            BookId = created.BookId,
            Title = created.Title,
            PublishYear = created.PublishDate.Year,
            Price = created.Price,
            AuthorFullName = $"{created.Author.FirstName} {created.Author.LastName}",
            CoverDesignIdeas = created.Cover?.DesignIdeas,
            ArtistFullNames = created.Cover?.Artists?
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToList()
        };

        return Ok(result);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        if (id != dto.BookId) return BadRequest("ID mismatch");

        var existingBook = await _bookRepo.GetByIdAsync(id);
        if (existingBook == null) return NotFound();

        existingBook.Title = dto.Title;
        existingBook.PublishDate = dto.PublishDate;
        existingBook.Price = dto.Price;
        existingBook.AuthorId = dto.AuthorId;
        //auto-mapper would be:  _mapper.Map(dto, existingBook);
        
        await _bookRepo.UpdateAsync(existingBook);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookRepo.GetByIdAsync(id);
        if (book == null) return NotFound();

        await _bookRepo.DeleteAsync(book);
        return NoContent();
    }
}
