using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

namespace SGB.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<EmployeeDto>> ListEmployeeAsync()
    {
       var employees = await _repository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Role = e.Role
            })
            .ToList();
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = dto.Password,
            Role = dto.Role
        };

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Phone = employee.Phone,
            Email = employee.Email,
            Role = employee.Role
        };
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, CreateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return null;

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.Role = dto.Role;

        await _repository.SaveChangesAsync();

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Phone = employee.Phone,
            Role = employee.Role
        };
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return false;

        await _repository.DeleteAsync(employee);
        await _repository.SaveChangesAsync();

        return true;
    }
}