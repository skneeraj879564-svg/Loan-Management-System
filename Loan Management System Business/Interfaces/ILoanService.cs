using Loan_Management_System_Business.Dtos.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ILoanService
    {
        // =====================================================
        // GET BY ID
        // =====================================================

        Task<LoanResponseDto?> GetByIdAsync(
            int loanId);


        // =====================================================
        // GET ALL
        // =====================================================

        Task<List<LoanResponseDto>> GetAllAsync();


        // =====================================================
        // GET BY LOAN APPLICATION
        // =====================================================

        Task<LoanResponseDto?> GetByLoanApplicationIdAsync(
            int loanApplicationId);


        // =====================================================
        // GET LOANS BY CUSTOMER
        // =====================================================

        Task<List<LoanResponseDto>> GetByCustomerIdAsync(
            int customerId);


        // =====================================================
        // CREATE
        // =====================================================

        Task<LoanResponseDto> CreateAsync(
            CreateLoanDto model);


        // =====================================================
        // UPDATE
        // =====================================================

        Task<LoanResponseDto?> UpdateAsync(
            int loanId,
            UpdateLoanDto model);


        // =====================================================
        // DELETE
        // =====================================================

        Task<bool> DeleteAsync(
            int loanId);
    }
}