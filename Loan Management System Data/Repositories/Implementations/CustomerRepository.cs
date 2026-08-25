using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Loan_Management_System_Data.Repositories.Implementations
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get customer by Customer Id
        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.EmploymentDetail)
                .Include(x => x.BankAccount)
                .Include(x => x.Nominee)
                .Include(x => x.KycDocuments)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        // Get customer by Identity User Id
        public async Task<Customer?> GetByUserIdAsync(string userId)
        {
            return await _context.Customers
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.EmploymentDetail)
                .Include(x => x.BankAccount)
                .Include(x => x.Nominee)
                .Include(x => x.KycDocuments)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        // Get all customers
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.EmploymentDetail)
                .Include(x => x.BankAccount)
                .Include(x => x.Nominee)
                .Include(x => x.KycDocuments)
                .ToListAsync();
        }

        // Add new customer
        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        // Update customer
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        // Delete customer
        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        // Check customer exists
        public async Task<bool> ExistsAsync(int customerId)
        {
            return await _context.Customers
                .AnyAsync(x => x.CustomerId == customerId);
        }

    }
}
