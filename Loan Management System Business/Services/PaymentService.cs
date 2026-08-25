using Loan_Management_System_Business.Dtos.Payment;
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
    public class PaymentService:IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(
            IPaymentRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<PaymentResponseDto?>
            GetByIdAsync(int paymentId)
        {
            var payment =
                await _repository.GetByIdAsync(paymentId);

            if (payment == null)
            {
                return null;
            }

            return MapToResponse(payment);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<PaymentResponseDto>>
            GetAllAsync()
        {
            var payments =
                await _repository.GetAllAsync();

            return payments
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<PaymentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var payments =
                await _repository.GetByLoanApplicationIdAsync(
                    loanApplicationId);

            return payments
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // =========================

        public async Task<List<PaymentResponseDto>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId)
        {
            var payments =
                await _repository.GetByLoanRepaymentIdAsync(
                    loanRepaymentId);

            return payments
                .Select(MapToResponse)
                .ToList();
        }

       
        // =========================
        // CREATE
        // =========================

        public async Task<PaymentResponseDto>
            CreateAsync(MakePaymentDto model)
        {
            // Get Loan Repayment
            var repayment =
                await _repository.GetLoanRepaymentAsync(
                    model.LoanRepaymentId);

            if (repayment == null)
            {
                throw new Exception(
                    "Loan repayment not found.");
            }

            // Create Payment
            var payment = new Payment
            {
                LoanApplicationId =
                    model.LoanApplicationId,

                LoanRepaymentId =
                    model.LoanRepaymentId,

                PaymentAmount =
                    model.PaymentAmount,

                PaymentDate =
                    model.PaymentDate,

                PaymentMethod =
                    model.PaymentMethod,

                TransactionId =
                    model.TransactionId,

                Remarks =
                    model.Remarks,

                Status = "Success"
            };

            // =========================
            // UPDATE LOAN REPAYMENT
            // =========================

            repayment.PaidAmount +=
                model.PaymentAmount;

            repayment.PaymentDate =
                model.PaymentDate;

            if (repayment.PaidAmount >= repayment.EMIAmount)
            {
                repayment.Status = "Paid";
            }
            else
            {
                repayment.Status = "Pending";
            }

            // Save Payment
            var result =
                await _repository.AddAsync(payment);

            return MapToResponse(result);
        }
        // =========================
        // UPDATE
        // =========================

        public async Task<PaymentResponseDto?>
            UpdateAsync(
                int paymentId,
                MakePaymentDto model)
        {
            var payment =
                await _repository.GetByIdAsync(paymentId);

            if (payment == null)
            {
                return null;
            }

            payment.LoanApplicationId =
                model.LoanApplicationId;

            payment.LoanRepaymentId =
                model.LoanRepaymentId;

            payment.PaymentAmount =
                model.PaymentAmount;

            payment.PaymentDate =
                model.PaymentDate;

            payment.PaymentMethod =
                model.PaymentMethod;

            payment.TransactionId =
                model.TransactionId;

            payment.Remarks =
                model.Remarks;

            var result =
                await _repository.UpdateAsync(payment);

            return MapToResponse(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int paymentId)
        {
            return await _repository.DeleteAsync(paymentId);
        }

        // =========================
        // MAPPING
        // =========================

        private static PaymentResponseDto
            MapToResponse(Payment payment)
        {
            return new PaymentResponseDto
            {
                PaymentId =
                    payment.PaymentId,

                LoanApplicationId =
                    payment.LoanApplicationId,

                LoanRepaymentId =
                    payment.LoanRepaymentId,

                PaymentAmount =
                    payment.PaymentAmount,

                PaymentDate =
                    payment.PaymentDate,

                PaymentMethod =
                    payment.PaymentMethod,

                TransactionId =
                    payment.TransactionId,

                Status =
                    payment.Status,

                Remarks =
                    payment.Remarks
            };
        }
    }
}
