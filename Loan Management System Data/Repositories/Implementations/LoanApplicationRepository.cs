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
    public class LoanApplicationRepository:ILoanApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanApplication?> GetByIdAsync(
            int loanApplicationId)
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .Include(x => x.ApprovedByEmployee)
                .FirstOrDefaultAsync(
                    x => x.LoanApplicationId == loanApplicationId);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanApplication>> GetAllAsync()
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .Include(x => x.ApprovedByEmployee)
                .ToListAsync();
        }


        // =========================
        // GET BY CUSTOMER ID
        // =========================

        public async Task<List<LoanApplication>> GetByCustomerIdAsync(
            int customerId)
        {
            return await _context.LoanApplications
                .Include(x => x.Customer)
                .Include(x => x.LoanProduct)
                .Include(x => x.ApprovedByEmployee)
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }


        // =========================
        // ADD
        // =========================

        public async Task<LoanApplication> AddAsync(
            LoanApplication loanApplication)
        {
            await _context.LoanApplications.AddAsync(
                loanApplication);

            await _context.SaveChangesAsync();

            return loanApplication;
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<LoanApplication> UpdateAsync(
            LoanApplication loanApplication)
        {
            _context.LoanApplications.Update(
                loanApplication);

            await _context.SaveChangesAsync();

            return loanApplication;
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanApplicationId)
        {
            var loanApplication =
                await _context.LoanApplications
                    .FirstOrDefaultAsync(
                        x => x.LoanApplicationId ==
                             loanApplicationId);

            if (loanApplication == null)
            {
                return false;
            }

            _context.LoanApplications.Remove(
                loanApplication);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ApproveAsync(
    int loanApplicationId,
    int approvedByEmployeeId)
        {
            var application = await _context.LoanApplications
                .FirstOrDefaultAsync(x =>
                    x.LoanApplicationId == loanApplicationId);

            if (application == null)
            {
                return false;
            }

            application.Status = "Approved";
            application.ApprovalDate = DateTime.UtcNow;
            application.ApprovedByEmployeeId = approvedByEmployeeId;
            application.RejectionReason = null;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> RejectAsync(
            int loanApplicationId,
            int rejectedByEmployeeId,
            string rejectionReason)
        {
            var application = await _context.LoanApplications
                .FirstOrDefaultAsync(x =>
                    x.LoanApplicationId == loanApplicationId);

            if (application == null)
            {
                return false;
            }

            application.Status = "Rejected";
            application.ApprovalDate = null;
            application.ApprovedByEmployeeId = rejectedByEmployeeId;
            application.RejectionReason = rejectionReason;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

