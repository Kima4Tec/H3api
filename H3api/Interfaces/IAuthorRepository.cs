using H3api.Entities;

namespace H3api.Interfaces
{
    //Using interface to avoid spelling mistakes and to ensure consistency across repositories.
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
        Task<bool> ExistsAsync(int id);
    }
}
