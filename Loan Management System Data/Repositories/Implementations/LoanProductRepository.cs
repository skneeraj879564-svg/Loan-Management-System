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
    public class LoanProductRepository: ILoanProductRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanProductRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanProduct?> GetByIdAsync(
            int loanProductId)
        {
            return await _context.LoanProducts
                .FirstOrDefaultAsync(
                    x => x.LoanProductId == loanProductId);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanProduct>> GetAllAsync()
        {
            return await _context.LoanProducts
                .ToListAsync();
        }


        // =========================
        // ADD
        // =========================

        public async Task<LoanProduct> AddAsync(
            LoanProduct loanProduct)
        {
            await _context.LoanProducts.AddAsync(
                loanProduct);

            await _context.SaveChangesAsync();

            return loanProduct;
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<LoanProduct> UpdateAsync(
            LoanProduct loanProduct)
        {
            _context.LoanProducts.Update(
                loanProduct);

            await _context.SaveChangesAsync();

            return loanProduct;
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanProductId)
        {
            var loanProduct =
                await _context.LoanProducts
                    .FirstOrDefaultAsync(
                        x => x.LoanProductId == loanProductId);

            if (loanProduct == null)
            {
                return false;
            }

            _context.LoanProducts.Remove(
                loanProduct);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
