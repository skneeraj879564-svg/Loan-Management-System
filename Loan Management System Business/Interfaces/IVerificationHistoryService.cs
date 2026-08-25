using Loan_Management_System_Business.Dtos.VerificationHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IVerificationHistoryService
    {
        Task<VerificationHistoryResponseDto?> GetByIdAsync(
      int verificationHistoryId);

        Task<List<VerificationHistoryResponseDto>>
            GetAllAsync();

        Task<List<VerificationHistoryResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<VerificationHistoryResponseDto?> CreateAsync(
            CreateVerificationHistoryDto model);

        Task<VerificationHistoryResponseDto?> UpdateAsync(
            int verificationHistoryId,
            UpdateVerificationHistoryDto model);

        Task<bool> DeleteAsync(
            int verificationHistoryId);
    }
}
