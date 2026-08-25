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
    public class LoanDocumentRepository:ILoanDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanDocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanDocument?> GetByIdAsync(
            int loanDocumentId)
        {
            return await _context.LoanDocuments
                .Include(x => x.LoanApplication)
                .Include(x => x.VerifiedByEmployee)
                .FirstOrDefaultAsync(
                    x => x.LoanDocumentId == loanDocumentId);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanDocument>> GetAllAsync()
        {
            return await _context.LoanDocuments
                .Include(x => x.LoanApplication)
                .Include(x => x.VerifiedByEmployee)
                .ToListAsync();
        }


        // =========================
        // GET BY APPLICATION
        // =========================

        public async Task<List<LoanDocument>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.LoanDocuments
                .Where(x =>
                    x.LoanApplicationId == loanApplicationId)
                .ToListAsync();
        }


        // =========================
        // ADD
        // =========================

        public async Task<LoanDocument> AddAsync(
            LoanDocument loanDocument)
        {
            await _context.LoanDocuments.AddAsync(
                loanDocument);

            await _context.SaveChangesAsync();

            return loanDocument;
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<LoanDocument> UpdateAsync(
            LoanDocument loanDocument)
        {
            _context.LoanDocuments.Update(
                loanDocument);

            await _context.SaveChangesAsync();

            return loanDocument;
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanDocumentId)
        {
            var document =
                await _context.LoanDocuments
                    .FirstOrDefaultAsync(
                        x => x.LoanDocumentId ==
                             loanDocumentId);

            if (document == null)
            {
                return false;
            }

            _context.LoanDocuments.Remove(document);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
