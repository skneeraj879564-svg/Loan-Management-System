using Loan_Management_System_Business.Dtos.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IBranchService
    {
        Task<BranchResponseDto> CreateAsync(CreateBranchDto model);

        Task<BranchResponseDto?> GetByIdAsync(int branchId);

        Task<List<BranchResponseDto>> GetAllAsync();

        Task<BranchResponseDto?> UpdateAsync(
            int branchId,
            UpdateBranchDto model);

        Task<bool> DeleteAsync(int branchId);
    }
}
