using H3api.Entities;

//Using interface to avoid spelling mistakes and to ensure consistency across repositories.
public interface ICoverRepository
{
    Task<IEnumerable<Cover>> GetAllAsync();
    Task<Cover?> GetByIdAsync(int id);
    Task<Cover> AddAsync(Cover cover);
    Task UpdateAsync(Cover cover);
    Task DeleteAsync(Cover cover);
}
