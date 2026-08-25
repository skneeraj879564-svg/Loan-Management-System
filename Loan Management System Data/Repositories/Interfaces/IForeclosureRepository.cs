using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IForeclosureRepository
    {
        Task<Foreclosure?> GetByIdAsync(
          int foreclosureId);

        Task<List<Foreclosure>> GetAllAsync();

        Task<List<Foreclosure>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<Foreclosure> AddAsync(
            Foreclosure foreclosure);

        Task<Foreclosure> UpdateAsync(
            Foreclosure foreclosure);

        Task<bool> DeleteAsync(
            int foreclosureId);
    }
}
