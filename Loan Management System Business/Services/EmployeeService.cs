using Loan_Management_System_Business.Dtos;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // =========================
        // GET EMPLOYEE BY ID
        // =========================

        public async Task<EmployeeResponseDto?> GetByIdAsync(
            int employeeId)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }

            return MapToResponse(employee);
        }


        // =========================
        // GET MY PROFILE
        // =========================

        public async Task<EmployeeResponseDto?> GetMyProfileAsync(
            string userId)
        {
            var employee =
                await _employeeRepository.GetByUserIdAsync(userId);

            if (employee == null)
            {
                return null;
            }

            return MapToResponse(employee);
        }


        // =========================
        // GET ALL EMPLOYEES
        // =========================

        public async Task<List<EmployeeResponseDto>> GetAllAsync()
        {
            var employees =
                await _employeeRepository.GetAllAsync();

            return employees
                .Select(MapToResponse)
                .ToList();
        }


        // =========================
        // CREATE EMPLOYEE
        // =========================

        public async Task<EmployeeResponseDto> CreateAsync(
            CreateEmployeeDto model)
        {
            var employee = new EmployeeProfile
            {
                UserId = model.UserId,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                EmployeeCode = model.EmployeeCode,
                Designation = model.Designation,
                Department = model.Department,
                BranchId = model.BranchId,
                JoiningDate = model.JoiningDate,
                Salary = model.Salary,
                IsActive = model.IsActive
            };

            var result =
                await _employeeRepository.AddAsync(employee);

            return MapToResponse(result);
        }


        // =========================
        // UPDATE EMPLOYEE
        // =========================

        public async Task<EmployeeResponseDto?> UpdateAsync(
            int employeeId,
            CreateEmployeeDto model)
        {
            var employee =
                await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return null;
            }

            employee.FullName = model.FullName;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Gender = model.Gender;
            employee.DateOfBirth = model.DateOfBirth;
            employee.EmployeeCode = model.EmployeeCode;
            employee.Designation = model.Designation;
            employee.Department = model.Department;
            employee.BranchId = model.BranchId;
            employee.JoiningDate = model.JoiningDate;
            employee.Salary = model.Salary;
            employee.IsActive = model.IsActive;

            var result =
                await _employeeRepository.UpdateAsync(employee);

            return MapToResponse(result);
        }


        // =========================
        // DELETE EMPLOYEE
        // =========================

        public async Task<bool> DeleteAsync(
            int employeeId)
        {
            return await _employeeRepository
                .DeleteAsync(employeeId);
        }


        // =========================
        // MAPPING
        // =========================

        private static EmployeeResponseDto MapToResponse(
            EmployeeProfile employee)
        {
            return new EmployeeResponseDto
            {
                EmployeeId = employee.EmployeeId,
                UserId = employee.UserId,
                FullName = employee.FullName,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                EmployeeCode = employee.EmployeeCode,
                Designation = employee.Designation,
                Department = employee.Department,
                BranchId = employee.BranchId,
                BranchName = employee.Branch?.BranchName,
                JoiningDate = employee.JoiningDate,
                LeavingDate = employee.LeavingDate,
                Salary = employee.Salary,
                IsActive = employee.IsActive
            };
        }

    }
}
