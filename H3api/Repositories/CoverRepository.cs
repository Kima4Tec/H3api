using H3api.Data;
using H3api.Entities;
using Microsoft.EntityFrameworkCore;

public class CoverRepository : ICoverRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor for CoverRepository. Initializes the repository with the provided ApplicationDbContext. 
    /// Written in the most recent style.
    /// </summary>
    /// <param name="context"></param>
    public CoverRepository(ApplicationDbContext context) => _context = context;

    /// <summary>
    /// Retrieves all covers from the database, including their associated books and artists.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Cover>> GetAllAsync() =>
        await _context.Covers.Include(c => c.Book).Include(c => c.Artists).ToListAsync();

    /// <summary>
    /// Retrieves a cover by its ID from the database, including its associated book and artists.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Cover?> GetByIdAsync(int id) =>
        await _context.Covers.Include(c => c.Book).Include(c => c.Artists)
            .FirstOrDefaultAsync(c => c.CoverId == id);

    public async Task<Cover> AddAsync(Cover cover)
    {
        _context.Covers.Add(cover);
        await _context.SaveChangesAsync();
        return cover;
    }

    public async Task UpdateAsync(Cover cover)
    {
        _context.Covers.Update(cover);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Cover cover)
    {
        _context.Covers.Remove(cover);
        await _context.SaveChangesAsync();
    }
}
