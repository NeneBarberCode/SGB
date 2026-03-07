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

    public async Task<BorrowingResponseDto> RegisterBorrowingAsync(int customerId, int copyId)
    {
        var customer = await _context.Customers
            .Include(c => c.Borrowings)
            .FirstOrDefaultAsync(c => c.Id == customerId);

        if (customer == null) throw new Exception("Cliente no encontrado.");

        // check for delays
        var delays = customer.Borrowings
            .Where(p => !p.ReturnDate.HasValue && 
                        p.BorrowDate.AddDays(freedays) < DateTime.UtcNow)
            .ToList();

        if (delays.Any()) throw new Exception("Cliente tiene préstamos retrasados, no puede tomar más libros.");

        // check active borrowings
        var activeBorrowings = customer.Borrowings.Count(b => !b.ReturnDate.HasValue);
        if (activeBorrowings >= MaxBorrowings) throw new Exception($"Cliente tiene {MaxBorrowings} préstamos activos.");

        // check availability of the copy
        var copy = await _context.Copies.FirstOrDefaultAsync(e => e.Id == copyId && e.Available);
        if (copy == null) throw new Exception("Ejemplar no disponible.");

        copy.Available = false;

        var borrowing = new Borrowing
        {
            CustomerId = customerId,
            CopyId = copyId,
            BorrowDate = DateTime.UtcNow,
            AccumulatedFee = 0
        };

        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();

        return _mapper.Map<BorrowingResponseDto>(borrowing);
    }
}
