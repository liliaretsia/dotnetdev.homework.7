using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public interface ICustomerRepository
    {
        Task<Customer> FindCustomerByIdAsync(long id);

        Task AddAsync(Customer customer);

        Task<int> SaveChangesAsync();
    }
}