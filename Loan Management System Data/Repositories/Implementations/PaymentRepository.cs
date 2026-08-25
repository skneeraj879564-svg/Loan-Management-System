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
    public class PaymentRepository:IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<Payment?> GetByIdAsync(
            int paymentId)
        {
            return await _context.Payments
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanRepayment)
                .FirstOrDefaultAsync(
                    x => x.PaymentId == paymentId);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(x => x.LoanApplication)
                .Include(x => x.LoanRepayment)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<Payment>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            return await _context.Payments
                .Where(x =>
                    x.LoanApplicationId == loanApplicationId)
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // =========================

        public async Task<List<Payment>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId)
        {
            return await _context.Payments
                .Where(x =>
                    x.LoanRepaymentId == loanRepaymentId)
                .OrderByDescending(x => x.PaymentDate)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================

        public async Task<Payment> AddAsync(
            Payment payment)
        {
            await _context.Payments.AddAsync(payment);

            await _context.SaveChangesAsync();

            return payment;
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<Payment> UpdateAsync(
            Payment payment)
        {
            _context.Payments.Update(payment);

            await _context.SaveChangesAsync();

            return payment;
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int paymentId)
        {
            var payment =
                await _context.Payments
                    .FirstOrDefaultAsync(
                        x => x.PaymentId == paymentId);

            if (payment == null)
            {
                return false;
            }

            _context.Payments.Remove(payment);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LoanRepayment?> GetLoanRepaymentAsync(int loanRepaymentId)
        {
            return await _context.LoanRepayments
                .FirstOrDefaultAsync(
                    x => x.LoanRepaymentId == loanRepaymentId);
        }
    }
}
