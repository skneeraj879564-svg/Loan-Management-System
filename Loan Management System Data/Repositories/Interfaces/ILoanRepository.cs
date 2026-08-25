using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        // =====================================================
        // GET BY ID
        // =====================================================

        Task<Loan?> GetByIdAsync(
            int loanId);


        // =====================================================
        // GET ALL
        // =====================================================

        Task<List<Loan>> GetAllAsync();


        // =====================================================
        // GET BY LOAN APPLICATION
        // =====================================================

        Task<Loan?> GetByLoanApplicationIdAsync(
            int loanApplicationId);


        // =====================================================
        // GET BY CUSTOMER
        // =====================================================

        Task<List<Loan>> GetByCustomerIdAsync(
            int customerId);


        // =====================================================
        // ADD
        // =====================================================

        Task<Loan> AddAsync(
            Loan loan);


        // =====================================================
        // UPDATE
        // =====================================================

        Task<Loan> UpdateAsync(
            Loan loan);


        // =====================================================
        // DELETE
        // =====================================================

        Task<bool> DeleteAsync(
            int loanId);
    }
}