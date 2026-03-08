using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> ListEmployeeAsync();
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
    }
}
