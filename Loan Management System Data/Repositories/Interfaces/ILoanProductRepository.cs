using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface ILoanProductRepository
    {
        Task<LoanProduct?> GetByIdAsync(int loanProductId);

        Task<List<LoanProduct>> GetAllAsync();

        Task<LoanProduct> AddAsync(LoanProduct loanProduct);

        Task<LoanProduct> UpdateAsync(LoanProduct loanProduct);

        Task<bool> DeleteAsync(int loanProductId);
    }
}
