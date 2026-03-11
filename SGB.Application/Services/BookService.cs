using Microsoft.EntityFrameworkCore;
using SGB.Application.DTOs.Auth;
using SGB.Application.Services.Interfaces;
using SGB.Infrastructure.Persistence;
using SGB.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace SGB.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;   
    }
    
        public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _repository.AddAsync(book);
            await _repository.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteBookAsync(int id)
        {
             var book = await _repository.GetByIdAsync(id);

              if (book == null) 
              throw new KeyNotFoundException($"Libro con id {id} no encontrado");

              await _repository.DeleteAsync(book);
              await _repository.SaveChangesAsync();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null) throw new KeyNotFoundException($"Libro con id {id} no encontrado");

           return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> ListBooksAsync()
        {
            var books = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> UpdateBookAsync(CreateBookDto dto, int id)
        {
          var book = await _repository.GetByIdAsync(id);
          
         if (book == null) throw new KeyNotFoundException($"Libro con id {id} no encontrado");
         _mapper.Map(dto, book);
         await _repository.SaveChangesAsync();
         return _mapper.Map<BookDto>(book);  
      
        }

    }
    }