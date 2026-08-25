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
    public class ForeclosureRepository:IForeclosureRepository
    {
        private readonly ApplicationDbContext _context;

        public ForeclosureRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<Foreclosure?> GetByIdAsync(
            int foreclosureId)
        {
            return await _context.Foreclosures
                .Include(x => x.LoanApplication)
                .FirstOrDefaultAsync(
                    x => x.ForeclosureId == foreclosureId);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<Foreclosure>> GetAllAsync()
        {
            return await _context.Foreclosures
                .Include(x => x.LoanApplication)
                .OrderByDescending(x => x.ForeclosureDate)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<Foreclosure>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.Foreclosures
                .Where(x =>
                    x.LoanApplicationId ==
                    loanApplicationId)
                .OrderByDescending(x => x.ForeclosureDate)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================

        public async Task<Foreclosure> AddAsync(
            Foreclosure foreclosure)
        {
            await _context.Foreclosures.AddAsync(
                foreclosure);

            await _context.SaveChangesAsync();

            return foreclosure;
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<Foreclosure> UpdateAsync(
            Foreclosure foreclosure)
        {
            _context.Foreclosures.Update(
                foreclosure);

            await _context.SaveChangesAsync();

            return foreclosure;
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int foreclosureId)
        {
            var foreclosure =
                await _context.Foreclosures
                    .FirstOrDefaultAsync(
                        x => x.ForeclosureId ==
                             foreclosureId);

            if (foreclosure == null)
            {
                return false;
            }

            _context.Foreclosures.Remove(
                foreclosure);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
