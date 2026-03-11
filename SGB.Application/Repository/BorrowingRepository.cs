using Microsoft.EntityFrameworkCore;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

public class BorrowingRepository : IBorrowingRepository
{
    private readonly SgbDbContext _context;

    public BorrowingRepository(SgbDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountActiveBorrowingsAsync(int customerId)
    {
        return await _context.Borrowings
            .CountAsync(b => b.CustomerId == customerId && b.ReturnDate == null);
    }

    public async Task<bool> HasDelaysAsync(int customerId)
    {
        return await _context.Borrowings
            .AnyAsync(b => b.CustomerId == customerId &&
                           b.ReturnDate == null &&
                           DateTime.Now > b.LimitDate);
    }

    public async Task<Copy?> GetCopyWithBookAsync(int copyId)
    {
        return await _context.Copies
            .Include(c => c.Book)
            .FirstOrDefaultAsync(c => c.Id == copyId);
    }

    public async Task AddBorrowingAsync(Borrowing borrowing)
    {
        await _context.Borrowings.AddAsync(borrowing);
    }

    public async Task<Borrowing?> GetBorrowingWithRelationsAsync(int borrowingId)
    {
        return await _context.Borrowings
            .Include(b => b.Copy)
                .ThenInclude(c => c.Book)
            .Include(b => b.Customer)
            .FirstOrDefaultAsync(b => b.Id == borrowingId);
    }

    public async Task<List<Borrowing>> GetAllBorrowingsAsync()
    {
        return await _context.Borrowings
            .Include(b => b.Customer)
            .Include(b => b.Copy)
                .ThenInclude(c => c.Book)
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}