using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Service {
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(long id);

        Task<long> AddCustomerAsync(string firstname, string lastname);
    }
}

