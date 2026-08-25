using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface ILoanRepaymentRepository
    {
        Task<LoanRepayment?> GetByIdAsync(int loanRepaymentId);

        Task<List<LoanRepayment>> GetAllAsync();

        Task<List<LoanRepayment>> GetByLoanApplicationIdAsync(
            int loanApplicationId);

        Task<LoanRepayment> AddAsync(
            LoanRepayment loanRepayment);

        Task<List<LoanRepayment>> AddRangeAsync(
            List<LoanRepayment> loanRepayments);

        Task<LoanRepayment> UpdateAsync(
            LoanRepayment loanRepayment);

        Task<bool> DeleteAsync(
            int loanRepaymentId);
    }
}
