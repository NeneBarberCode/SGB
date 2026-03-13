using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Domain.Entities;
namespace SGB.Application.Services;

public class BorrowingService : IBorrowingService
{
    
    
    private readonly IBorrowingRepository _repository;
    private readonly IConfigurationService _service;

    public BorrowingService(IBorrowingRepository repository, IConfigurationService configurationService)
    {
        _repository = repository;
        _service = configurationService;
    }

    // crete borroing with validations: max 5 active borrowings, no delays, copy available
   public async Task<BorrowingResponseDto> CreateBorrowingAsync(int customerId, int copyId)
{
    
    int activeBorrowings = await _repository.CountActiveBorrowingsAsync(customerId);

    if (activeBorrowings >= 5)
        throw new InvalidOperationException("El cliente ya tiene 5 préstamos activos.");

    bool delays = await _repository.HasDelaysAsync(customerId);
      
    if (delays)
        throw new InvalidOperationException("El cliente tiene préstamos retrasados.");

     var copy = await _repository.GetCopyWithBookAsync(copyId);

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

    await _repository.AddBorrowingAsync(borrowing);
    await _repository.SaveChangesAsync();
    
    var customer = await _repository.GetCustomerAsync(customerId);

    return new BorrowingResponseDto
    {
         Id = borrowing.Id,
            Customer = customer!.Name,
            Book = copy.Book.Title,
            BorrowDate = borrowing.BorrowDate,
            LimitDate = borrowing.LimitDate,
            ReturnDate = borrowing.ReturnDate,
            AccumulatedFee = borrowing.AccumulatedFee
    };
}


    // return and calculate accumulated fee if there is a delay
   public async Task<BorrowingResponseDto> ReturnBorrowingAsync(int borrowingId)
{
     var borrowing = await _repository.GetBorrowingWithRelationsAsync(borrowingId);

    if (borrowing == null)
        throw new InvalidOperationException("Préstamo no encontrado.");

    if (borrowing.ReturnDate != null)
        throw new InvalidOperationException("Préstamo ya devuelto.");

    borrowing.ReturnDate = DateTime.Now;
     
     var config = await _service.GetAsync();
    decimal DailyFee = config.DailyFee;

    int delayedDays = 0;

    if (borrowing.ReturnDate > borrowing.LimitDate)
        delayedDays = (borrowing.ReturnDate.Value - borrowing.LimitDate).Days;

    borrowing.AccumulatedFee = delayedDays * DailyFee;

    borrowing.Copy.Available = true;

     await _repository.SaveChangesAsync();

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
     var borrowings = await _repository.GetAllBorrowingsAsync();

    return borrowings.Select(p => new BorrowingResponseDto
        {
            Id = p.Id,
            Customer = p.Customer.Name,
            Book = p.Copy.Book.Title,
            BorrowDate = p.BorrowDate,
            LimitDate = p.LimitDate,
            ReturnDate = p.ReturnDate,
            AccumulatedFee = p.AccumulatedFee
        })
        .ToList();
}

}
