using WebApi.Models;
using WebApi.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Service {
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomerAsync(long id)
        {
            return await _customerRepository.FindCustomerByIdAsync(id);
        }

        public async Task<long> AddCustomerAsync(string firstname, string lastname)
        {
            var customer = new Customer
            {
                Firstname = firstname,
                Lastname = lastname
            };

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return customer.Id;
        }
    }
}