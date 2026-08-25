using Loan_Management_System_Business.Dtos.LoanRepayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ILoanRepaymentService
    {
        // GET BY ID
        Task<LoanRepaymentResponseDto?>
            GetByIdAsync(int loanRepaymentId);

        // GET ALL
        Task<List<LoanRepaymentResponseDto>>
            GetAllAsync();

        // GET BY LOAN APPLICATION
        Task<List<LoanRepaymentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        // CREATE
        Task<LoanRepaymentResponseDto>
            CreateAsync(
                CreateLoanRepaymentDto model);

        // UPDATE
        Task<LoanRepaymentResponseDto?>
            UpdateAsync(
                int loanRepaymentId,
                UpdateLoanRepaymentDto model);

        // DELETE
        Task<bool>
            DeleteAsync(int loanRepaymentId);
    }
}
