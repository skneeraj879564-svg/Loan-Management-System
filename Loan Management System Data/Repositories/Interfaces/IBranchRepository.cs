using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Interfaces
{
    public interface IBranchRepository
    {
        Task<Branch> CreateAsync(Branch branch);

        Task<Branch?> GetByIdAsync(int branchId);

        Task<List<Branch>> GetAllAsync();

        Task<Branch?> UpdateAsync(Branch branch);

        Task<bool> DeleteAsync(int branchId);
    }
}
