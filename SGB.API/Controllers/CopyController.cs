using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGB.Infrastructure.Persistence;
using SGB.Domain.Entities;
using SGB.Application.DTOs.Auth;

namespace SGB.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CopyController : ControllerBase
{
private readonly SgbDbContext _context;
    public CopyController(SgbDbContext  context)
    {
        _context = context;
    }

    // GET: api/ejemplares
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ejemplares = await _context.Copies
            .Include(e => e.Book)
            .Select(e => new CopyDto
            {
                Id = e.Id,
                BookId = e.BookId,
                BookTitle = e.Book.Title,
                Available = e.Available
            })
            .ToListAsync();

        return Ok(ejemplares);
    }

    // GET: api/ejemplares/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var copy = await _context.Copies
            .Include(e => e.Book)
            .Where(e => e.Id == id)
            .Select(e => new CopyDto
            {
                Id = e.Id,
                BookId = e.BookId,
                BookTitle = e.Book.Title,
                Available = e.Available
            })
            .FirstOrDefaultAsync();

        if (copy == null)
            return NotFound();

        return Ok(copy);
    }

    // POST: api/ejemplares
    [HttpPost]
    public async Task<IActionResult> Create(CreateCopyDto dto)
    {
        var bookExists = await _context.Books
            .AnyAsync(l => l.Id == dto.BookId);

        if (!bookExists)
            return BadRequest("El libro no existe.");

        var copy = new Copy
        {
            BookId = dto.BookId,
            Available = true
        };

        _context.Copies.Add(copy);
        await _context.SaveChangesAsync();

        return Ok(copy.Id);
    }

    // PUT: api/ejemplares/5
    [HttpPut("{id}")]
    public async Task<IActionResult> CambiarEstado(int id, bool available)
    {
        var copy = await _context.Copies.FindAsync(id);

        if (copy == null)
            return NotFound();

        copy.Available = available;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/ejemplares/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var copy = await _context.Copies.FindAsync(id);

        if (copy == null)
            return NotFound();

        _context.Copies.Remove(copy);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
