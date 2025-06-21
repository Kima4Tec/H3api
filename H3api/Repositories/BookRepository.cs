using H3api.Data;
using H3api.Entities;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;
    public BookRepository(ApplicationDbContext context) => _context = context;

    /// <summary>
    /// Retrieves all books from the database, including their associated authors and covers. 
    /// AsNoTracking is used to improve performance by not tracking the entities in the context for changes.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.Include(b => b.Author)
                             .Include(b => b.Cover)
                             .ThenInclude(c => c.Artists)
                             .AsNoTracking()
                             .ToListAsync();

    public async Task<Book?> GetByIdAsync(int id) =>
        await _context.Books.Include(b => b.Author)
                             .Include(b => b.Cover)
                             .ThenInclude(c => c.Artists)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(b => b.BookId == id);

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id) =>
        await _context.Books.AnyAsync(b => b.BookId == id);

    public async Task<Book?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Cover) 
                .ThenInclude(c => c.Artists)
                .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BookId == id);
    }
}
