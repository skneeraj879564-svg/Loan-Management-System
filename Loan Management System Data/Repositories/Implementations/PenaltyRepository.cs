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
    public class PenaltyRepository:IPenaltyRepository
    {
        private readonly ApplicationDbContext _context;

        public PenaltyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<Penalty?> GetByIdAsync(
            int penaltyId)
        {
            return await _context.Penalties
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanRepayment)
                .FirstOrDefaultAsync(
                    x => x.PenaltyId == penaltyId);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<Penalty>> GetAllAsync()
        {
            return await _context.Penalties
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanRepayment)
                .OrderByDescending(x => x.PenaltyDate)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<Penalty>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.Penalties
                .Where(x =>
                    x.LoanApplicationId ==
                    loanApplicationId)
                .OrderByDescending(x => x.PenaltyDate)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // =========================

        public async Task<List<Penalty>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId)
        {
            return await _context.Penalties
                .Where(x =>
                    x.LoanRepaymentId ==
                    loanRepaymentId)
                .OrderByDescending(x => x.PenaltyDate)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================

        public async Task<Penalty> AddAsync(
            Penalty penalty)
        {
            await _context.Penalties.AddAsync(penalty);

            await _context.SaveChangesAsync();

            return penalty;
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<Penalty> UpdateAsync(
            Penalty penalty)
        {
            _context.Penalties.Update(penalty);

            await _context.SaveChangesAsync();

            return penalty;
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int penaltyId)
        {
            var penalty =
                await _context.Penalties
                    .FirstOrDefaultAsync(
                        x => x.PenaltyId == penaltyId);

            if (penalty == null)
            {
                return false;
            }

            _context.Penalties.Remove(penalty);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
