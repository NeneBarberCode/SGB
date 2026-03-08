using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _service;

    public BorrowingController(IBorrowingService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDto dto)
    {
        try
        {
            var borrowing = await _service.CreateBorrowingAsync(dto.CustomerId, dto.CopyId);
            return Ok(borrowing);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id}/devolver")]
    public async Task<IActionResult> ReturnBorrowing(int id)
    {
        try
        {
            var borrowing = await _service.ReturnBorrowingAsync(id);
            return Ok(borrowing);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBorrowings()
    {
        var borrowings = await _service.ListBorrowingsAsync();
        return Ok(borrowings);
    }
}
