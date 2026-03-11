using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> ListEmployeeAsync();
        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);

        Task<EmployeeDto?> UpdateEmployeeAsync(int id, CreateEmployeeDto dto);

        Task<bool> DeleteEmployeeAsync(int id);
    }
}
