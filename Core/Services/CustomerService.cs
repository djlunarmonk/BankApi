using AutoMapper;
using BankApi.Core.Interfaces;
using BankApi.Data.Interfaces;
using BankApi.Domain.DTO;

namespace BankApi.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<string>> GetAllCustomers()
        {
            var list = await _repo.GetAllCustomers();
            var result = new List<string>();
            foreach (var c in list)
            {
                result.Add(_mapper.Map<CustomerDTO>(c).ToString());
            }
            return result;
        }
    }
}
