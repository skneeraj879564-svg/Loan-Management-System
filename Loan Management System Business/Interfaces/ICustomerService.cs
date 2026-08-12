using Loan_Management_System_Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ICustomerService
    {
        // Get customer by Customer Id
        Task<CustomerDto?> GetByIdAsync(int customerId);

        // Get customer by logged-in User Id
        Task<CustomerDto?> GetByUserIdAsync(string userId);

        // Get all customers
        Task<List<CustomerDto>> GetAllAsync();

        // Create new customer
        Task<CustomerDto> CreateAsync(
            string userId,
            CreateCustomerDto model);

        // Update existing customer
        Task<CustomerDto?> UpdateAsync(
            int customerId,
            UpdateCustomerDto model);

        // Delete customer
        Task<bool> DeleteAsync(int customerId);
    }
}
