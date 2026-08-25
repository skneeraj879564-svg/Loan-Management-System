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
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        // =====================================================
        // GET BY ID
        // =====================================================

        public async Task<Loan?> GetByIdAsync(
            int loanId)
        {
            return await _context.Loans
                .Include(x => x.LoanApplication)
                .FirstOrDefaultAsync(
                    x => x.LoanId == loanId);
        }


        // =====================================================
        // GET ALL
        // =====================================================

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .Include(x => x.LoanApplication)
                .ToListAsync();
        }


        // =====================================================
        // GET BY LOAN APPLICATION
        // =====================================================

        public async Task<Loan?> GetByLoanApplicationIdAsync(
            int loanApplicationId)
        {
            return await _context.Loans
                .Include(x => x.LoanApplication)
                .FirstOrDefaultAsync(
                    x => x.LoanApplicationId
                         == loanApplicationId);
        }


        // =====================================================
        // GET BY CUSTOMER
        // =====================================================

        public async Task<List<Loan>> GetByCustomerIdAsync(
            int customerId)
        {
            return await _context.Loans
                .Include(x => x.LoanApplication)
                .Where(x =>
                    x.LoanApplication.CustomerId
                    == customerId)
                .ToListAsync();
        }


        // =====================================================
        // ADD
        // =====================================================

        public async Task<Loan> AddAsync(
            Loan loan)
        {
            await _context.Loans.AddAsync(loan);

            await _context.SaveChangesAsync();

            return loan;
        }


        // =====================================================
        // UPDATE
        // =====================================================

        public async Task<Loan> UpdateAsync(
            Loan loan)
        {
            _context.Loans.Update(loan);

            await _context.SaveChangesAsync();

            return loan;
        }


        // =====================================================
        // DELETE
        // =====================================================

        public async Task<bool> DeleteAsync(
            int loanId)
        {
            var loan =
                await _context.Loans
                    .FirstOrDefaultAsync(
                        x => x.LoanId == loanId);

            if (loan == null)
            {
                return false;
            }

            _context.Loans.Remove(loan);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}