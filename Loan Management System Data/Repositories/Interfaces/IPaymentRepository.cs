using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(int paymentId);

        Task<List<Payment>> GetAllAsync();

        Task<List<Payment>> GetByLoanApplicationIdAsync(
            int loanApplicationId);

        Task<List<Payment>> GetByLoanRepaymentIdAsync(
            int loanRepaymentId);

        Task<Payment> AddAsync(
            Payment payment);

        Task<Payment> UpdateAsync(
            Payment payment);

        Task<bool> DeleteAsync(
            int paymentId);
        Task<LoanRepayment?> GetLoanRepaymentAsync(
         int loanRepaymentId);
    }
}
