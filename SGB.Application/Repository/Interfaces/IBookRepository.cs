using SGB.Domain.Entities;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task<List<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task DeleteAsync(Book book);
    Task SaveChangesAsync();
}