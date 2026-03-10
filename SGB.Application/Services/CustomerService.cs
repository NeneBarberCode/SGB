using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;


namespace SGB.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly SgbDbContext _context;
    private readonly IMapper _mapper;

    private const int MaxBorrowings = 5;
    private const int freedays = 30;
    private const decimal Feeperday = 1.0m; // configurable 

    public CustomerService(SgbDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> ListCustomersAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
  
}
