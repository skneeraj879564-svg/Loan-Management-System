using Loan_Management_System_Business.Dtos.Penalty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IPenaltyService
    {
        Task<PenaltyResponseDto?> GetByIdAsync(
            int penaltyId);

        Task<List<PenaltyResponseDto>> GetAllAsync();

        Task<List<PenaltyResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<List<PenaltyResponseDto>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId);

        Task<PenaltyResponseDto>
            CreateAsync(
                int loanApplicationId,
                int loanRepaymentId,
                decimal penaltyAmount,
                DateTime penaltyDate,
                string reason);

        Task<PenaltyResponseDto?>
            UpdateAsync(
                int penaltyId,
                decimal penaltyAmount,
                DateTime penaltyDate,
                string reason,
                string status,
                DateTime? paidDate);

        Task<bool> DeleteAsync(
            int penaltyId);
    }
}
