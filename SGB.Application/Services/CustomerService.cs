using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;


namespace SGB.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _Repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _Repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        await _Repository.AddAsync(customer);
        await _Repository.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> ListCustomersAsync()
    {
        var customers = await _Repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
  
}
