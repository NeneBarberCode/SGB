using SGB.Domain.Entities;

public interface IBorrowingRepository
{
    Task<int> CountActiveBorrowingsAsync(int customerId);
    Task<bool> HasDelaysAsync(int customerId);

    Task<Copy?> GetCopyWithBookAsync(int copyId);

    Task AddBorrowingAsync(Borrowing borrowing);

    Task<Borrowing?> GetBorrowingWithRelationsAsync(int borrowingId);

    Task<List<Borrowing>> GetAllBorrowingsAsync();

    Task<Customer?> GetCustomerAsync(int customerId);

    Task SaveChangesAsync();
}