using H3api.Data;
using H3api.Entities;
using H3api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace H3api.Repositories
{
    /// <summary>
    /// Repository for managing Author entities in the database.
    /// </summary>
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor for AuthorRepository. Initializes the repository with the provided ApplicationDbContext.
        /// </summary>
        /// <param name="context"></param>
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
    }
}
