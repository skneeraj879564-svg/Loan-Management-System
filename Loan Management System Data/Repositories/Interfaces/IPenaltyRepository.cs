using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IPenaltyRepository
    {
        Task<Penalty?> GetByIdAsync(int penaltyId);

        Task<List<Penalty>> GetAllAsync();

        Task<List<Penalty>> GetByLoanApplicationIdAsync(
            int loanApplicationId);

        Task<List<Penalty>> GetByLoanRepaymentIdAsync(
            int loanRepaymentId);

        Task<Penalty> AddAsync(
            Penalty penalty);

        Task<Penalty> UpdateAsync(
            Penalty penalty);

        Task<bool> DeleteAsync(
            int penaltyId);
    }
}
