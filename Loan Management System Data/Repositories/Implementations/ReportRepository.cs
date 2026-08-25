using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Implementations
{
    public class ReportRepository:IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // LOAN REPORT
        // =========================

        public async Task<List<LoanApplication>> GetLoanReportsAsync()
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .ToListAsync();
        }


        // =========================
        // COLLECTION REPORT
        // =========================

        public async Task<List<Payment>> GetCollectionReportsAsync()
        {
            return await _context.Payments
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanRepayment)
                .ToListAsync();
        }


        // =========================
        // REJECTION REPORT
        // =========================

        public async Task<List<LoanApplication>> GetRejectionReportsAsync()
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .Where(x => x.Status == "Rejected")
                .ToListAsync();
        }


        // =========================
        // OVERDUE REPORT
        // =========================

        public async Task<List<LoanRepayment>> GetOverdueReportsAsync()
        {
            var today = DateTime.UtcNow;

            return await _context.LoanRepayments
                .Include(x => x.LoanApplication)
                .Where(x =>
                    x.DueDate < today &&
                    x.Status != "Paid")
                .ToListAsync();
        }


        // =========================
        // CUSTOMER STATEMENT
        // =========================

        public async Task<LoanApplication?> GetCustomerStatementAsync(
            int customerId)
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .Include(x => x.LoanRepayments)
                .FirstOrDefaultAsync(
                    x => x.CustomerId == customerId);
        }


        // =========================
        // PAYMENT HISTORY
        // =========================

        public async Task<List<Payment>> GetPaymentHistoryAsync(
            int loanApplicationId)
        {
            return await _context.Payments
                .Include(x => x.LoanRepayment)
                .Where(x =>
                    x.LoanApplicationId == loanApplicationId)
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }
    }
}
