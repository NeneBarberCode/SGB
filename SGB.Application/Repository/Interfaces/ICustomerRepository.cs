using SGB.Domain.Entities;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<List<Customer>> GetAllAsync();
    Task SaveChangesAsync();
}