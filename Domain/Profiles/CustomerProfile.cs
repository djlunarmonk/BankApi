using AutoMapper;
using BankApi.Domain.DTO;
using BankApi.Domain.Entities;

namespace BankApi.Domain.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<CustomerNewDTO, Customer>();
        }


    }
}
