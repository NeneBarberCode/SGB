using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{

public interface ICustomerService
{
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);
    Task<IEnumerable<CustomerDto>> ListCustomersAsync();
    Task<BorrowingResponseDto> RegisterBorrowingAsync(int customerId, int copyId);
}
}