using WebApi.Models;
using WebApi.Infrastructure.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Infrastructure.Repository {
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> FindCustomerByIdAsync(long id)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await _context.Customer.AddAsync(customer);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}