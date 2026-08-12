using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories
{
    public interface ICustomerRepository
    {
        // Get customer by Customer Id
        Task<Customer?> GetByIdAsync(int customerId);

        // Get customer by Identity User Id
        Task<Customer?> GetByUserIdAsync(string userId);

        // Get all customers
        Task<List<Customer>> GetAllAsync();

        // Add new customer
        Task<Customer> AddAsync(Customer customer);

        // Update customer
        Task<Customer> UpdateAsync(Customer customer);

        // Delete customer
        Task<bool> DeleteAsync(int customerId);

        // Check customer exists
        Task<bool> ExistsAsync(int customerId);
    }
}
