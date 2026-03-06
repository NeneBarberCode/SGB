using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Security;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SgbDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(SgbDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateEmployeeDto dto)
    {
        if (await _context.Employees.AnyAsync(e => e.Email == dto.Email))
            return BadRequest("Este Email ya está registrado.");

        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = PasswordHasher.Hash(dto.Password),
            Role = dto.Role
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok("Empleado registrado correctamente.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.Employees 
            .FirstOrDefaultAsync(e => e.Email == dto.Email);

        if (user == null)
            return Unauthorized("Credenciales inválidas.");

        if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Credenciales inválidas.");

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            token,
            user = new
            {
                id=user.Id,
                email =user.Email,
                role =user.Role
            }
        });
    }
}
