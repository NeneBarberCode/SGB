using SGB.Domain.Entities;

public interface IConfigurationRepository
{
    Task<Configuration?> GetAsync();
    Task<Configuration?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}