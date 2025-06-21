using H3api.Entities;

namespace H3api.Interfaces
{
    //Using interface to avoid spelling mistakes and to ensure consistency across repositories.
    public interface IArtistRepository
    {
        Task <IEnumerable<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        Task UpdateAsync(Artist artist);
        Task DeleteAsync(Artist artist);

    }
}
