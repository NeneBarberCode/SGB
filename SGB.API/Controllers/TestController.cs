using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("publico")]
    public IActionResult Publico()
    {
        return Ok("Endpoint público.");
    }

    [Authorize]
    [HttpGet("privado")]
    public IActionResult Privado()
    {
        return Ok("Accediste con token válido.");
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpGet("solo-superadmin")]
    public IActionResult SoloSuperAdmin()
    {
        return Ok("Accediste como SuperAdmin.");
    }
}
