using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs.Auth;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;
using SGB.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace SGB.API.Controllers;

[Authorize(Roles = "SuperAdmin")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly SgbDbContext _context;

    public AdminController(SgbDbContext context)
    {
        _context = context;
    }

    // Create employee
    [HttpPost("empleados")]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
    {
        var Employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = PasswordHasher.Hash(dto.Password),
            Role = dto.Role,
            Active = true
        };

        _context.Employees.Add(Employee);
        await _context.SaveChangesAsync();

        return Ok(new EmployeeDto
        {
            Id = Employee.Id,
            Name = Employee.Name,
            Email = Employee.Email,
            Role = Employee.Role,
            Active = Employee.Active
        });
    }

    // List employees
    [HttpGet("empleados")]
    public async Task<IActionResult> ListEployee()
    {
        var employee = await _context.Employees
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Role = e.Role,
                Active = e.Active
            })
            .ToListAsync();

        return Ok(employee);
    }

    // get configuration
    [HttpGet("configuracion")]
    public async Task<IActionResult> GetConfiguration()
    {
        var config = await _context.Configurations.FirstOrDefaultAsync();
        return Ok(config);
    }

    // update daily fee
    [HttpPut("configuracion/fee")]
    public async Task<IActionResult> UpdateDailyFee([FromBody] decimal newFee)
    {
        var config = await _context.Configurations.FirstOrDefaultAsync();

        if (config == null)
        {
            config = new Configuration { DailyFee = newFee };
            _context.Configurations.Add(config);
        }
        else
        {
            config.DailyFee = newFee;
        }

        await _context.SaveChangesAsync();
        return Ok(config);
    }
}
