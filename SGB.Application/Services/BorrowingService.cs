using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
using SGB.Infrastructure.Persistence;

namespace SGB.Application.Services;

public class BorrowingService : IBorrowingService
{
    
    
    private readonly SgbDbContext _context;

    public BorrowingService(SgbDbContext context)
    {
        _context = context;
    }

    // Crear préstamo
   public async Task<BorrowingResponseDto> CreateBorrowingAsync(int customerId, int copyId)
{
    
    int activeBorrowings = await _context.Borrowings
        .CountAsync(b => b.CustomerId == customerId && b.ReturnDate == null);

    if (activeBorrowings >= 5)
        throw new InvalidOperationException("El cliente ya tiene 5 préstamos activos.");

    bool delays = await _context.Borrowings
        .AnyAsync(b => b.CustomerId == customerId &&
                       b.ReturnDate == null &&
                       DateTime.Now > b.LimitDate);

    if (delays)
        throw new InvalidOperationException("El cliente tiene préstamos retrasados.");

    var copy = await _context.Copies
        .Include(c => c.Book)
        .FirstOrDefaultAsync(c => c.Id == copyId);

    if (copy == null)
        throw new InvalidOperationException("Ejemplar no encontrado.");

    if (!copy.Available)
        throw new InvalidOperationException("El ejemplar no está disponible.");

    var borrowing = new Borrowing
    {
        CustomerId = customerId,
        CopyId = copyId,
        BorrowDate = DateTime.Now,
        LimitDate = DateTime.Now.AddDays(7),
        AccumulatedFee = 0
    };

    copy.Available = false;

    _context.Borrowings.Add(borrowing);
    await _context.SaveChangesAsync();

    return new BorrowingResponseDto
    {
        Id = borrowing.Id,
        Customer = (await _context.Customers.FindAsync(customerId))!.Name,
        Book = copy.Book.Title,
        BorrowDate = borrowing.BorrowDate,
        LimitDate = borrowing.LimitDate,
        ReturnDate = borrowing.ReturnDate,
        AccumulatedFee = borrowing.AccumulatedFee
    };
}


    // Devolver ejemplar y calcular fee acumulado
   public async Task<BorrowingResponseDto> ReturnBorrowingAsync(int borrowingId)
{
    var borrowing = await _context.Borrowings
        .Include(b => b.Copy)
            .ThenInclude(c => c.Book)
        .Include(c => c.Customer)
        .FirstOrDefaultAsync(b => b.Id == borrowingId);

    if (borrowing == null)
        throw new InvalidOperationException("Préstamo no encontrado.");

    if (borrowing.ReturnDate != null)
        throw new InvalidOperationException("Préstamo ya devuelto.");

    borrowing.ReturnDate = DateTime.Now;

    int feeDiario = 1;

    int diasRetraso = 0;

    if (borrowing.ReturnDate > borrowing.LimitDate)
        diasRetraso = (borrowing.ReturnDate.Value - borrowing.LimitDate).Days;

    borrowing.AccumulatedFee = diasRetraso * feeDiario;

    borrowing.Copy.Available = true;

    await _context.SaveChangesAsync();

    return new BorrowingResponseDto
    {
        Id = borrowing.Id,
        Customer = borrowing.Customer.Name,
        Book = borrowing.Copy.Book.Title,
        BorrowDate = borrowing.BorrowDate,
        LimitDate = borrowing.LimitDate,
        ReturnDate = borrowing.ReturnDate,
        AccumulatedFee = borrowing.AccumulatedFee
    };
}

public async Task<IEnumerable<BorrowingResponseDto>> ListBorrowingsAsync()
{
    
    return await _context.Borrowings
        .Include(b => b.Customer)
        .Include(b => b.Copy)
            .ThenInclude(c => c.Book)
        .Select(p => new BorrowingResponseDto
        {
            Id = p.Id,
            Customer = p.Customer.Name,
            Book = p.Copy.Book.Title,
            BorrowDate = p.BorrowDate,
            LimitDate = p.LimitDate,
            ReturnDate = p.ReturnDate,
            AccumulatedFee = p.AccumulatedFee
        })
        .ToListAsync();
}

}
