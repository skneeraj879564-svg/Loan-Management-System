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
    public class VerificationHistoryRepository:IVerificationHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public VerificationHistoryRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<VerificationHistory?> GetByIdAsync(
            int verificationHistoryId)
        {
            return await _context.VerificationHistories
                .Include(x => x.LoanApplication)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(
                    x => x.VerificationHistoryId
                         == verificationHistoryId);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<VerificationHistory>>
            GetAllAsync()
        {
            return await _context.VerificationHistories
                .Include(x => x.LoanApplication)
                .Include(x => x.Employee)
                .ToListAsync();
        }


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<VerificationHistory>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.VerificationHistories
                .Where(x =>
                    x.LoanApplicationId == loanApplicationId)
                .OrderByDescending(x =>
                    x.VerificationDate)
                .ToListAsync();
        }


        // =========================
        // ADD
        // =========================

        public async Task<VerificationHistory> AddAsync(
            VerificationHistory verificationHistory)
        {
            await _context.VerificationHistories.AddAsync(
                verificationHistory);

            await _context.SaveChangesAsync();

            return verificationHistory;
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<VerificationHistory> UpdateAsync(
            VerificationHistory verificationHistory)
        {
            _context.VerificationHistories.Update(
                verificationHistory);

            await _context.SaveChangesAsync();

            return verificationHistory;
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int verificationHistoryId)
        {
            var history =
                await _context.VerificationHistories
                    .FirstOrDefaultAsync(
                        x => x.VerificationHistoryId
                             == verificationHistoryId);

            if (history == null)
            {
                return false;
            }

            _context.VerificationHistories.Remove(history);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
