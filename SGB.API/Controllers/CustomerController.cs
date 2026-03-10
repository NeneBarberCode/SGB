using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGB.Application.DTOs;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;

namespace SGB.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

}
