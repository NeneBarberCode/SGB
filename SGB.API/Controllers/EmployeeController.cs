using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    // Only SuperAdmin can see employees and create them
    [HttpGet]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> ListEmployees()
    {
        var employees = await _service.ListEmployeeAsync();
        return Ok(employees);
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
    {
        var employee = await _service.CreateEmployeeAsync(dto);
        return Ok(employee);
    }
}
