using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface ILoanDocumentRepository
    {
        Task<LoanDocument?> GetByIdAsync(int loanDocumentId);

        Task<List<LoanDocument>> GetAllAsync();

        Task<List<LoanDocument>> GetByLoanApplicationIdAsync(
            int loanApplicationId);

        Task<LoanDocument> AddAsync(
            LoanDocument loanDocument);

        Task<LoanDocument> UpdateAsync(
            LoanDocument loanDocument);

        Task<bool> DeleteAsync(
            int loanDocumentId);
    }
}
