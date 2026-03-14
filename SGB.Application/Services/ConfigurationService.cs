using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using AutoMapper;

namespace SGB.Application.Services;


public class ConfigurationService : IConfigurationService
{
    private readonly IConfigurationRepository _repository;
    private readonly IMapper _mapper;

    public ConfigurationService(IConfigurationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ConfigurationDto> GetAsync()
    {
       var config = await _repository.GetAsync();

      if (config == null)
        return new ConfigurationDto();

       return _mapper.Map<ConfigurationDto>(config);
    }

    public async Task UpdateAsync(ConfigurationDto dto)
    {
        var configuration = await _repository.GetByIdAsync(dto.Id);
        if (configuration == null)
            throw new InvalidOperationException("Configuración no encontrada.");

       _mapper.Map(dto, configuration);

        await _repository.SaveChangesAsync();
    }

}