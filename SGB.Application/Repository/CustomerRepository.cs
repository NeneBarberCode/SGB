using Microsoft.EntityFrameworkCore;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly SgbDbContext _context;

    public CustomerRepository(SgbDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}