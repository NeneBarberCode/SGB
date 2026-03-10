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
        private readonly SgbDbContext _context;
        private readonly IMapper _mapper;

    public BookService(SgbDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;   
    }
    
        public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task DeleteBookAsync(int id)
        {
             var book = await _context.Books.FindAsync(id);
              if (book == null) 
              throw new KeyNotFoundException($"Book with id {id} not found");
              _context.Books.Remove(book);
              await _context.SaveChangesAsync();
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new KeyNotFoundException($"Book with id {id} not found");

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN
            };
        }

        public async Task<IEnumerable<BookDto>> ListBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN
            });
        }

        public async Task<BookDto> UpdateBookAsync(CreateBookDto dto, int id)
        {
         var book = await _context.Books.FindAsync(id);
               if (book == null) throw new KeyNotFoundException($"Book with id {id} not found");
            _mapper.Map(dto, book);
      await _context.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);  
      
        }

    }
    }