using H3api.Entities;

//Using interface to avoid spelling mistakes and to ensure consistency across repositories.
public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<bool> ExistsAsync(int id);
    Task<Book?> GetByIdWithDetailsAsync(int id);
}