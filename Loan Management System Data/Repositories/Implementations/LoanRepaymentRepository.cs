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
    public class LoanRepaymentRepository:ILoanRepaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanRepayment?> GetByIdAsync(
            int loanRepaymentId)
        {
            return await _context.LoanRepayments
                .Include(x => x.LoanApplication)
                .FirstOrDefaultAsync(
                    x => x.LoanRepaymentId == loanRepaymentId);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanRepayment>> GetAllAsync()
        {
            return await _context.LoanRepayments
                .Include(x => x.LoanApplication)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<LoanRepayment>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.LoanRepayments
                .Where(x =>
                    x.LoanApplicationId == loanApplicationId)
                .OrderBy(x => x.InstallmentNumber)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================

        public async Task<LoanRepayment> AddAsync(
            LoanRepayment loanRepayment)
        {
            await _context.LoanRepayments.AddAsync(
                loanRepayment);

            await _context.SaveChangesAsync();

            return loanRepayment;
        }

        // =========================
        // ADD RANGE
        // =========================

        public async Task<List<LoanRepayment>> AddRangeAsync(
            List<LoanRepayment> loanRepayments)
        {
            await _context.LoanRepayments.AddRangeAsync(
                loanRepayments);

            await _context.SaveChangesAsync();

            return loanRepayments;
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<LoanRepayment> UpdateAsync(
            LoanRepayment loanRepayment)
        {
            _context.LoanRepayments.Update(
                loanRepayment);

            await _context.SaveChangesAsync();

            return loanRepayment;
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanRepaymentId)
        {
            var repayment =
                await _context.LoanRepayments
                    .FirstOrDefaultAsync(
                        x => x.LoanRepaymentId ==
                             loanRepaymentId);

            if (repayment == null)
            {
                return false;
            }

            _context.LoanRepayments.Remove(repayment);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
