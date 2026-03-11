using SGB.Domain.Entities;

public interface IEmployeeRepository
{
      Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task DeleteAsync(Employee employee);
    Task SaveChangesAsync();
}