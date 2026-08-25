using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCustomersAsync()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _context.EmployeeProfiles.CountAsync();
        }

        public async Task<int> GetTotalLoanApplicationsAsync()
        {
            return await _context.LoanApplications.CountAsync();
        }

        public async Task<int> GetPendingLoansAsync()
        {
            return await _context.LoanApplications
                .CountAsync(x => x.Status == "Pending");
        }

        public async Task<int> GetApprovedLoansAsync()
        {
            return await _context.LoanApplications
                .CountAsync(x => x.Status == "Approved");
        }

        public async Task<int> GetRejectedLoansAsync()
        {
            return await _context.LoanApplications
                .CountAsync(x => x.Status == "Rejected");
        }

        public async Task<int> GetTotalLoanRepaymentsAsync()
        {
            return await _context.LoanRepayments.CountAsync();
        }

        public async Task<int> GetTotalPaymentsAsync()
        {
            return await _context.Payments.CountAsync();
        }

        public async Task<decimal> GetTotalPaymentAmountAsync()
        {
            return await _context.Payments
                .SumAsync(x => (decimal?)x.PaymentAmount) ?? 0;
        }

        public async Task<decimal> GetTotalOutstandingAmountAsync()
        {
            return await _context.LoanRepayments
                .Where(x => x.Status != "Paid")
                .SumAsync(x => x.EMIAmount - x.PaidAmount);
        }
    }
}
