using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<EmployeeProfile?> GetByIdAsync(
            int employeeId)
        {
            return await _context.EmployeeProfiles
                .Include(e => e.Branch)
                .Include(e => e.User)
                .FirstOrDefaultAsync(
                    e => e.EmployeeId == employeeId);
        }

        // =========================
        // GET BY USER ID
        // =========================
        public async Task<EmployeeProfile?> GetByUserIdAsync(
            string userId)
        {
            return await _context.EmployeeProfiles
                .Include(e => e.Branch)
                .Include(e => e.User)
                .FirstOrDefaultAsync(
                    e => e.UserId == userId);
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<EmployeeProfile>> GetAllAsync()
        {
            return await _context.EmployeeProfiles
                .Include(e => e.Branch)
                .Include(e => e.User)
                .ToListAsync();
        }

        // =========================
        // ADD
        // =========================
        public async Task<EmployeeProfile> AddAsync(
            EmployeeProfile employee)
        {
            await _context.EmployeeProfiles.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<EmployeeProfile> UpdateAsync(
            EmployeeProfile employee)
        {
            _context.EmployeeProfiles.Update(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        // =========================
        // DELETE
        // =========================
        public async Task<bool> DeleteAsync(
            int employeeId)
        {
            var employee = await _context.EmployeeProfiles
                .FirstOrDefaultAsync(
                    e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return false;
            }

            _context.EmployeeProfiles.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
