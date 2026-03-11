using Microsoft.EntityFrameworkCore;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

public class ConfigurationRepository : IConfigurationRepository
{
    private readonly SgbDbContext _context;

    public ConfigurationRepository(SgbDbContext context)
    {
        _context = context;
    }

    public async Task<Configuration?> GetAsync()
    {
        return await _context.Configurations.FirstOrDefaultAsync();
    }

    public async Task<Configuration?> GetByIdAsync(int id)
    {
        return await _context.Configurations.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}