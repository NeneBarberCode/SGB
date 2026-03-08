using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGB.Infrastructure.Persistence;
using SGB.Domain.Entities;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly SgbDbContext _context;

    public BookController(SgbDbContext context)
    {
        _context = context;
    }

    // GET: api/libro
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
            .Include(l => l.Copies)
            .ToListAsync();
        return Ok(books);
    }

    // POST: api/libro
    [Authorize(Roles = "SuperAdmin,Bibliotecario")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // GET: api/libro/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _context.Books
            .Include(l => l.Copies)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (book == null) return NotFound();
        return Ok(book);
    }

    // PUT: api/libro/{id}
    [Authorize(Roles = "SuperAdmin,Bibliotecario")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Book updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.ISBN = updatedBook.ISBN;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/libro/{id}
    [Authorize(Roles = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
