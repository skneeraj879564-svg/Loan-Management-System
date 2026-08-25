using Loan_Management_System_Business.Dtos.VerificationHistory;
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
    public class VerificationHistoryService:IVerificationHistoryService
    {
        private readonly IVerificationHistoryRepository _repository;

        public VerificationHistoryService(
            IVerificationHistoryRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<VerificationHistoryResponseDto?>
            GetByIdAsync(int verificationHistoryId)
        {
            var data = await _repository
                .GetByIdAsync(verificationHistoryId);

            if (data == null)
                return null;

            return MapToResponseDto(data);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<VerificationHistoryResponseDto>>
            GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data
                .Select(MapToResponseDto)
                .ToList();
        }


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<VerificationHistoryResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var data = await _repository
                .GetByLoanApplicationIdAsync(
                    loanApplicationId);

            return data
                .Select(MapToResponseDto)
                .ToList();
        }


        // =========================
        // CREATE
        // =========================

        public async Task<VerificationHistoryResponseDto>
            CreateAsync(
                CreateVerificationHistoryDto model)
        {
            var entity = new VerificationHistory
            {
                LoanApplicationId = model.LoanApplicationId,
                EmployeeId = model.EmployeeId,
                VerificationType = model.VerificationType,
                Status = model.Status,
                Remarks = model.Remarks,
                CreditScore = model.CreditScore,
                VerificationDate = DateTime.UtcNow
            };

            var result = await _repository.AddAsync(entity);

            return MapToResponseDto(result);
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<VerificationHistoryResponseDto?>
            UpdateAsync(
                int verificationHistoryId,
                UpdateVerificationHistoryDto model)
        {
            var existing = await _repository
                .GetByIdAsync(verificationHistoryId);

            if (existing == null)
                return null;

            existing.VerificationType =
                model.VerificationType;

            existing.Status =
                model.Status;

            existing.Remarks =
                model.Remarks;

            existing.CreditScore =
                model.CreditScore;

            var result = await _repository
                .UpdateAsync(existing);

            return MapToResponseDto(result);
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int verificationHistoryId)
        {
            return await _repository
                .DeleteAsync(verificationHistoryId);
        }


        // =========================
        // MAPPING
        // =========================

        private static VerificationHistoryResponseDto
            MapToResponseDto(
                VerificationHistory data)
        {
            return new VerificationHistoryResponseDto
            {
                VerificationHistoryId =
                    data.VerificationHistoryId,

                LoanApplicationId =
                    data.LoanApplicationId,

                EmployeeId =
                    data.EmployeeId,

                VerificationType =
                    data.VerificationType,

                Status =
                    data.Status,

                Remarks =
                    data.Remarks,

                CreditScore =
                    data.CreditScore,

                VerificationDate =
                    data.VerificationDate
            };
        }
    }
}
