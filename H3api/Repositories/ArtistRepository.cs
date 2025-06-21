using H3api.Interfaces;
using H3api.Data;
using H3api.Entities;
using Microsoft.EntityFrameworkCore;

namespace H3api.Repositories
{
    /// <summary>
    /// Repository for managing Artist entities in the database.
    /// </summary>
    public class ArtistRepository: IArtistRepository
    {
        private readonly ApplicationDbContext _context;
        public ArtistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public Task AddAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Artist artist)
        {
            _context.Artists.Remove(artist);
            return _context.SaveChangesAsync();
        }


        public Task UpdateAsync(Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
