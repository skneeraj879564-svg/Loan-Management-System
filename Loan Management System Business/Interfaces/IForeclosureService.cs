using Loan_Management_System_Business.Dtos.Foreclosure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IForeclosureService
    {
        Task<ForeclosureDto?> GetByIdAsync(
            int foreclosureId);

        Task<List<ForeclosureDto>> GetAllAsync();

        Task<List<ForeclosureDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<ForeclosureDto>
            CreateAsync(
                ForeclosureDto model);

        Task<ForeclosureDto?>
            UpdateAsync(
                int foreclosureId,
                ForeclosureDto model);

        Task<bool> DeleteAsync(
            int foreclosureId);
    }
}
