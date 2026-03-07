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

            CreateMap<Borrowing, BorrowingResponseDto>();
        }
    }
}
