using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGB.Infrastructure.Persistence;
using SGB.Domain.Entities;
using SGB.Application.Services.Interfaces;
using SGB.Application.DTOs.Auth;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
     private readonly IBookService _service;

    public BookController( IBookService service)
    {
        _service = service;
    }

    // GET: api/libro
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var book = await _service.ListBooksAsync();
           
        return Ok(book);
    }

    // POST: api/libro
    [Authorize(Roles = "SuperAdmin,Bibliotecario")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Book book)
    {        var createdBook = await _service.CreateBookAsync(new CreateBookDto
        {
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN
        });
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    // GET: api/libro/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);
    }

    // PUT: api/libro/{id}
    [Authorize(Roles = "SuperAdmin,Bibliotecario")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update( int id,[FromBody] CreateBookDto dto)
    {
        var updatedBook = await _service.UpdateBookAsync(dto, id);
        return Ok(updatedBook);
    }

    // DELETE: api/libro/{id}
    [Authorize(Roles = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
       await _service.DeleteBookAsync(id);
        return NoContent();
    }
}
