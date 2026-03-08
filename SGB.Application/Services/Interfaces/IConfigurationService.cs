using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<ConfigurationDto> GetAsync();
        Task UpdateAsync(ConfigurationDto dto);
    }
}
