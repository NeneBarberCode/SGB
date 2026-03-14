using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
using AutoMapper;

namespace SGB.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper )
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> ListEmployeeAsync()
    {
       var employees = await _repository.GetAllAsync();
       return _mapper.Map<List<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
       var employee = _mapper.Map<Employee>(dto);

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, CreateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return null;

       _mapper.Map(dto, employee);

        await _repository.SaveChangesAsync();

         return _mapper.Map<EmployeeDto>(employee);
         
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