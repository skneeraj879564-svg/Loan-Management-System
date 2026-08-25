using Loan_Management_System_Business.Dtos.Penalty;
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
    public class PenaltyService:IPenaltyService
    {
        private readonly IPenaltyRepository _repository;

        public PenaltyService(
            IPenaltyRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<PenaltyResponseDto?>
            GetByIdAsync(int penaltyId)
        {
            var penalty =
                await _repository.GetByIdAsync(penaltyId);

            if (penalty == null)
            {
                return null;
            }

            return MapToResponse(penalty);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<PenaltyResponseDto>>
            GetAllAsync()
        {
            var penalties =
                await _repository.GetAllAsync();

            return penalties
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<PenaltyResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var penalties =
                await _repository.GetByLoanApplicationIdAsync(
                    loanApplicationId);

            return penalties
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // =========================

        public async Task<List<PenaltyResponseDto>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId)
        {
            var penalties =
                await _repository.GetByLoanRepaymentIdAsync(
                    loanRepaymentId);

            return penalties
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public async Task<PenaltyResponseDto>
            CreateAsync(
                int loanApplicationId,
                int loanRepaymentId,
                decimal penaltyAmount,
                DateTime penaltyDate,
                string reason)
        {
            var penalty = new Penalty
            {
                LoanApplicationId =
                    loanApplicationId,

                LoanRepaymentId =
                    loanRepaymentId,

                PenaltyAmount =
                    penaltyAmount,

                PenaltyDate =
                    penaltyDate,

                Reason =
                    reason,

                Status = "Pending",

                PaidDate = null
            };

            var result =
                await _repository.AddAsync(penalty);

            return MapToResponse(result);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<PenaltyResponseDto?>
            UpdateAsync(
                int penaltyId,
                decimal penaltyAmount,
                DateTime penaltyDate,
                string reason,
                string status,
                DateTime? paidDate)
        {
            var penalty =
                await _repository.GetByIdAsync(
                    penaltyId);

            if (penalty == null)
            {
                return null;
            }

            penalty.PenaltyAmount =
                penaltyAmount;

            penalty.PenaltyDate =
                penaltyDate;

            penalty.Reason =
                reason;

            penalty.Status =
                status;

            penalty.PaidDate =
                paidDate;

            var result =
                await _repository.UpdateAsync(
                    penalty);

            return MapToResponse(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int penaltyId)
        {
            return await _repository.DeleteAsync(
                penaltyId);
        }

        // =========================
        // MAPPING
        // =========================

        private static PenaltyResponseDto
            MapToResponse(Penalty penalty)
        {
            return new PenaltyResponseDto
            {
                PenaltyId =
                    penalty.PenaltyId,

                LoanApplicationId =
                    penalty.LoanApplicationId,

                LoanRepaymentId =
                    penalty.LoanRepaymentId,

                PenaltyAmount =
                    penalty.PenaltyAmount,

                PenaltyDate =
                    penalty.PenaltyDate,

                Reason =
                    penalty.Reason,

                Status =
                    penalty.Status,

                PaidDate =
                    penalty.PaidDate
            };
        }

    }
}
