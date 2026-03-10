using SGB.Application.DTOs.Auth;

namespace SGB.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<BookDto> CreateBookAsync(CreateBookDto dto );
        Task<IEnumerable<BookDto>> ListBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task<BookDto> UpdateBookAsync(CreateBookDto dto, int id);
        Task DeleteBookAsync(int id);




    }
}
