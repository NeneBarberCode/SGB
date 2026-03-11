using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Infrastructure.Persistence;

namespace SGB.Application.Services;


public class ConfigurationService : IConfigurationService
{
    private readonly SgbDbContext _context;

    public ConfigurationService(SgbDbContext context)
    {
        _context = context;
    }

    public async Task<ConfigurationDto> GetAsync()
    {
        return await _context.Configurations
            .Select(c => new ConfigurationDto
            {
                Id = c.Id,
               DailyFee = c.DailyFee,
            })
            .FirstOrDefaultAsync() ?? new ConfigurationDto();
    }

    public async Task UpdateAsync(ConfigurationDto dto)
    {
        var configuration = await _context.Configurations.FindAsync(dto.Id);
        if (configuration == null)
            throw new InvalidOperationException("Configuración no encontrada.");

        configuration.DailyFee = dto.DailyFee;

        await _context.SaveChangesAsync();
    }

}