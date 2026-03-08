using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
namespace SGB.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _service;

    public ConfigurationController(IConfigurationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var config = await _service.GetAsync();
        return Ok(config);
    }

    [HttpPut]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Update([FromBody] ConfigurationDto dto)
    {
        await _service.UpdateAsync(dto);
        return NoContent();
    }
}
