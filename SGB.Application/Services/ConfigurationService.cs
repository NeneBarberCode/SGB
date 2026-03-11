using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Infrastructure.Persistence;

namespace SGB.Application.Services;


public class ConfigurationService : IConfigurationService
{
    private readonly IConfigurationRepository _repository;

    public ConfigurationService(IConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConfigurationDto> GetAsync()
    {
           var config = await _repository.GetAsync();
            return config == null ? new ConfigurationDto() : new ConfigurationDto
            {
                Id = config.Id,
                DailyFee = config.DailyFee,
            };
    }

    public async Task UpdateAsync(ConfigurationDto dto)
    {
        var configuration = await _repository.GetByIdAsync(dto.Id);
        if (configuration == null)
            throw new InvalidOperationException("Configuración no encontrada.");

        configuration.DailyFee = dto.DailyFee;

        await _repository.SaveChangesAsync();
    }

}