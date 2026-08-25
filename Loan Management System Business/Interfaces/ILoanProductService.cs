using Loan_Management_System_Business.Dtos.LoanProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface ILoanProductService
    {
        // Get Loan Product by ID
        Task<LoanProductResponseDto?> GetByIdAsync(
            int loanProductId);

        // Get All Loan Products
        Task<List<LoanProductResponseDto>> GetAllAsync();

        // Create Loan Product
        Task<LoanProductResponseDto> CreateAsync(
            CreateLoanProductDto model);

        // Update Loan Product
        Task<LoanProductResponseDto?> UpdateAsync(
            int loanProductId,
            UpdateLoanProductDto model);

        // Delete Loan Product
        Task<bool> DeleteAsync(
            int loanProductId);
    }
}
