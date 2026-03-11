using Microsoft.EntityFrameworkCore;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

public class BookRepository : IBookRepository
{
    private readonly SgbDbContext _context;

    public BookRepository(SgbDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}