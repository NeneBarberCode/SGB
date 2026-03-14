using AutoMapper;
using SGB.Application.DTOs.Auth;
using SGB.Domain.Entities;

namespace SGB.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
              CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<Borrowing, BorrowingResponseDto>();
            CreateMap<Borrowing, BorrowingResponseDto>()
                .ForMember(dest => dest.Customer, 
                    opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Book, 
                    opt => opt.MapFrom(src => src.Copy.Book.Title));
                    
         CreateMap<Configuration, ConfigurationDto>().ReverseMap();

         CreateMap<Employee, EmployeeDto>();

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom(src => src.Password));
        }
    }
}
