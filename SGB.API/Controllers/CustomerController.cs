using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
    {
        var customer = await _service.CreateCustomerAsync(dto);
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> ListCustomers()
    {
        var customers = await _service.ListCustomersAsync();
        return Ok(customers);
    }

    [HttpPost("{customerId}/borrowing/{copyId}")]
    public async Task<IActionResult> RegisterBorrowing(int customerId, int copyId)
    {
        try
        {
            var borrowing = await _service.RegisterBorrowingAsync(customerId, copyId);
            return Ok(borrowing);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
