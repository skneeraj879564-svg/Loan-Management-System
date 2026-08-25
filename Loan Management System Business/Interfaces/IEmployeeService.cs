using Loan_Management_System_Business.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Loan_Management_System_Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeResponseDto?> GetByIdAsync(
           int employeeId);

        Task<EmployeeResponseDto?> GetMyProfileAsync(
            string userId);

        Task<List<EmployeeResponseDto>> GetAllAsync();

        Task<EmployeeResponseDto> CreateAsync(
            CreateEmployeeDto model);

        Task<EmployeeResponseDto?> UpdateAsync(
            int employeeId,
            CreateEmployeeDto model);

        Task<bool> DeleteAsync(
            int employeeId);
    }
}
