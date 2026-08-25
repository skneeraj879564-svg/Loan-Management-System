using Loan_Management_System_Data.Data;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Repositories.Implementations
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _context;

        public BranchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> CreateAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();

            return branch;
        }

        public async Task<Branch?> GetByIdAsync(int branchId)
        {
            return await _context.Branches
                .FirstOrDefaultAsync(x => x.BranchId == branchId);
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            return await _context.Branches
                .OrderBy(x => x.BranchName)
                .ToListAsync();
        }

        public async Task<Branch?> UpdateAsync(Branch branch)
        {
            var existingBranch = await _context.Branches
                .FirstOrDefaultAsync(x => x.BranchId == branch.BranchId);

            if (existingBranch == null)
            {
                return null;
            }

            existingBranch.BranchName = branch.BranchName;
            existingBranch.Address = branch.Address;
            existingBranch.City = branch.City;
            existingBranch.State = branch.State;
            existingBranch.PinCode = branch.PinCode;
            existingBranch.PhoneNumber = branch.PhoneNumber;
            existingBranch.Email = branch.Email;
            existingBranch.IsActive = branch.IsActive;

            await _context.SaveChangesAsync();

            return existingBranch;
        }

        public async Task<bool> DeleteAsync(int branchId)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(x => x.BranchId == branchId);

            if (branch == null)
            {
                return false;
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
