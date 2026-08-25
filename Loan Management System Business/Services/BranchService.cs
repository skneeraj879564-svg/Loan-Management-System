using Loan_Management_System_Business.Dtos.Branch;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        // ==========================================
        // CREATE BRANCH
        // ==========================================
        public async Task<BranchResponseDto> CreateAsync(
            CreateBranchDto model)
        {
            var branch = new Branch
            {
                BranchName = model.BranchName,
                Address = model.Address,
                City = model.City,
                State = model.State,
                PinCode = model.PinCode,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                IsActive = true
            };

            var result = await _branchRepository.CreateAsync(branch);

            return MapToResponse(result);
        }

        // ==========================================
        // GET BY ID
        // ==========================================
        public async Task<BranchResponseDto?> GetByIdAsync(
            int branchId)
        {
            var branch =
                await _branchRepository.GetByIdAsync(branchId);

            if (branch == null)
            {
                return null;
            }

            return MapToResponse(branch);
        }

        // ==========================================
        // GET ALL
        // ==========================================
        public async Task<List<BranchResponseDto>> GetAllAsync()
        {
            var branches =
                await _branchRepository.GetAllAsync();

            return branches
                .Select(MapToResponse)
                .ToList();
        }

        // ==========================================
        // UPDATE
        // ==========================================
        public async Task<BranchResponseDto?> UpdateAsync(
            int branchId,
            UpdateBranchDto model)
        {
            var branch =
                await _branchRepository.GetByIdAsync(branchId);

            if (branch == null)
            {
                return null;
            }

            branch.BranchName = model.BranchName;
            branch.Address = model.Address;
            branch.City = model.City;
            branch.State = model.State;
            branch.PinCode = model.PinCode;
            branch.PhoneNumber = model.PhoneNumber;
            branch.Email = model.Email;
            branch.IsActive = model.IsActive;

            var updatedBranch =
                await _branchRepository.UpdateAsync(branch);

            if (updatedBranch == null)
            {
                return null;
            }

            return MapToResponse(updatedBranch);
        }

        // ==========================================
        // DELETE
        // ==========================================
        public async Task<bool> DeleteAsync(int branchId)
        {
            return await _branchRepository.DeleteAsync(branchId);
        }

        // ==========================================
        // MAPPING
        // ==========================================
        private static BranchResponseDto MapToResponse(
            Branch branch)
        {
            return new BranchResponseDto
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                Address = branch.Address,
                City = branch.City,
                State = branch.State,
                PinCode = branch.PinCode,
                PhoneNumber = branch.PhoneNumber,
                Email = branch.Email,
                IsActive = branch.IsActive
            };
        }
    }
}
