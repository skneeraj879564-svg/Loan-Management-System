using Loan_Management_System_Business.Dtos.LoanApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ILoanApplicationService
    {
        // Get application by ID
        Task<LoanApplicationResponseDto?> GetByIdAsync(
            int loanApplicationId);

        // Get all applications
        Task<List<LoanApplicationResponseDto>> GetAllAsync();

        // Get applications of a customer
        Task<List<LoanApplicationResponseDto>> GetByCustomerIdAsync(
            int customerId);

        // Create application
        Task<LoanApplicationResponseDto> CreateAsync(
            CreateLoanApplicationDto model);

        // Update application
        Task<LoanApplicationResponseDto?> UpdateAsync(
            int loanApplicationId,
            UpdateLoanApplicationDto model);

        // Delete application
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
