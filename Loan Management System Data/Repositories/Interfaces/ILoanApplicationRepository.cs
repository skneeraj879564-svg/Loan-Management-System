using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<LoanApplication?> GetByIdAsync(int loanApplicationId);

        Task<List<LoanApplication>> GetAllAsync();

        Task<List<LoanApplication>> GetByCustomerIdAsync(
            int customerId);

        Task<LoanApplication> AddAsync(
            LoanApplication loanApplication);

        Task<LoanApplication> UpdateAsync(
            LoanApplication loanApplication);

        Task<bool> DeleteAsync(
            int loanApplicationId);
        Task<bool> ApproveAsync(
    int loanApplicationId,
    int approvedByEmployeeId);

        Task<bool> RejectAsync(
            int loanApplicationId,
            int rejectedByEmployeeId,
            string rejectionReason);
    }
}
