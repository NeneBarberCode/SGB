using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{
    public interface IBorrowingService
    {
        Task<IEnumerable<BorrowingResponseDto>> ListBorrowingsAsync();
        Task<BorrowingResponseDto> CreateBorrowingAsync(int customerId, int copyId);
        Task<BorrowingResponseDto> ReturnBorrowingAsync(int borrowingId);
    }
}
