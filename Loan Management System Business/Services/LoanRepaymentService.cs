using Loan_Management_System_Business.Dtos.LoanRepayment;
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
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly ILoanRepaymentRepository _repository;

        public LoanRepaymentService(
            ILoanRepaymentRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanRepaymentResponseDto?>
            GetByIdAsync(int loanRepaymentId)
        {
            var repayment =
                await _repository.GetByIdAsync(loanRepaymentId);

            if (repayment == null)
            {
                return null;
            }

            return MapToResponse(repayment);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanRepaymentResponseDto>>
            GetAllAsync()
        {
            var repayments =
                await _repository.GetAllAsync();

            return repayments
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<LoanRepaymentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var repayments =
                await _repository.GetByLoanApplicationIdAsync(
                    loanApplicationId);

            return repayments
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public async Task<LoanRepaymentResponseDto>
            CreateAsync(CreateLoanRepaymentDto model)
        {
            var repayment = new LoanRepayment
            {
                LoanApplicationId =
                    model.LoanApplicationId,

                InstallmentNumber =
                    model.InstallmentNumber,

                DueDate =
                    model.DueDate,

                EMIAmount =
                    model.EMIAmount,

                PrincipalAmount =
                    model.PrincipalAmount,

                InterestAmount =
                    model.InterestAmount,

                PaidAmount =
                    model.PaidAmount,

                PaymentDate =
                    model.PaymentDate,

                Status =
                    model.Status
            };

            var result =
                await _repository.AddAsync(repayment);

            return MapToResponse(result);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<LoanRepaymentResponseDto?>
            UpdateAsync(
                int loanRepaymentId,
                UpdateLoanRepaymentDto model)
        {
            var repayment =
                await _repository.GetByIdAsync(
                    loanRepaymentId);

            if (repayment == null)
            {
                return null;
            }

            repayment.DueDate =
                model.DueDate;

            repayment.EMIAmount =
                model.EMIAmount;

            repayment.PrincipalAmount =
                model.PrincipalAmount;

            repayment.InterestAmount =
                model.InterestAmount;

            repayment.PaidAmount =
                model.PaidAmount;

            repayment.PaymentDate =
                model.PaymentDate;

            repayment.Status =
                model.Status;

            var result =
                await _repository.UpdateAsync(
                    repayment);

            return MapToResponse(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int loanRepaymentId)
        {
            return await _repository.DeleteAsync(
                loanRepaymentId);
        }

        // =========================
        // MAPPING
        // =========================

        private static LoanRepaymentResponseDto
            MapToResponse(LoanRepayment repayment)
        {
            return new LoanRepaymentResponseDto
            {
                LoanRepaymentId =
                    repayment.LoanRepaymentId,

                LoanApplicationId =
                    repayment.LoanApplicationId,

                InstallmentNumber =
                    repayment.InstallmentNumber,

                DueDate =
                    repayment.DueDate,

                EMIAmount =
                    repayment.EMIAmount,

                PrincipalAmount =
                    repayment.PrincipalAmount,

                InterestAmount =
                    repayment.InterestAmount,

                PaidAmount =
                    repayment.PaidAmount,

                PaymentDate =
                    repayment.PaymentDate,

                Status =
                    repayment.Status
            };
        }

    }
}
