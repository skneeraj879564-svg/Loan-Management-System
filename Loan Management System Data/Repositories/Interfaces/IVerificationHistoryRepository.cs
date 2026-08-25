using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IVerificationHistoryRepository
    {
        Task<VerificationHistory?> GetByIdAsync(
           int verificationHistoryId);

        Task<List<VerificationHistory>> GetAllAsync();

        Task<List<VerificationHistory>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<VerificationHistory> AddAsync(
            VerificationHistory verificationHistory);

        Task<VerificationHistory> UpdateAsync(
            VerificationHistory verificationHistory);

        Task<bool> DeleteAsync(
            int verificationHistoryId);
    }
}
