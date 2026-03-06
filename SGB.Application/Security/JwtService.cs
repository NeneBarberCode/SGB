using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SGB.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGB.Application.Security;

public class JwtService
{
    
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;

    }
    public string GenerateToken(Employee employee)
    {
        // Console.WriteLine("ISSUER GENERATION: " + _configuration["JwtSettings:Issuer"]);
        // Console.WriteLine("AUDIENCE GENERATION: " + _configuration["JwtSettings:Audience"]);
        // Console.WriteLine("KEY GENERATION: " + _configuration["JwtSettings:Key"]);
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new Claim(ClaimTypes.Name, employee.Name),
            new Claim(ClaimTypes.Email, employee.Email),
            new Claim(ClaimTypes.Role, employee.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["JwtSettings:DurationInMinutes"]!)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
