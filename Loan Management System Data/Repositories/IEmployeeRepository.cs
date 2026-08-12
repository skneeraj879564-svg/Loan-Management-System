using Loan_Management_System_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeProfile?> GetByIdAsync(int employeeId);

        Task<EmployeeProfile?> GetByUserIdAsync(string userId);

        Task<List<EmployeeProfile>> GetAllAsync();

        Task<EmployeeProfile> AddAsync(
            EmployeeProfile employee);

        Task<EmployeeProfile> UpdateAsync(
            EmployeeProfile employee);

        Task<bool> DeleteAsync(int employeeId);
    }
}
