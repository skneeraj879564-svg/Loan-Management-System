using Loan_Management_System_Business.Dtos.Reports;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Services
{
    public class ReportService:IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // LOAN REPORT
        // =========================

        public async Task<List<LoanReportDto>> GetLoanReportsAsync()
        {
            var loans = await _repository.GetLoanReportsAsync();

            return loans.Select(x => new LoanReportDto
            {
                LoanApplicationId = x.LoanApplicationId,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer?.FullName ?? string.Empty,
                LoanProductName = x.LoanProduct?.ProductName ?? string.Empty,
                RequestedAmount = x.RequestedAmount,
                Status = x.Status,
                ApplicationDate = x.ApplicationDate
            }).ToList();
        }


        // =========================
        // COLLECTION REPORT
        // =========================

        public async Task<List<CollectionReportDto>> GetCollectionReportsAsync()
        {
            var payments = await _repository.GetCollectionReportsAsync();

            return payments.Select(x => new CollectionReportDto
            {
                PaymentId = x.PaymentId,
                LoanApplicationId = x.LoanApplicationId,
                LoanRepaymentId = x.LoanRepaymentId,
                PaymentAmount = x.PaymentAmount,
                PaymentDate = x.PaymentDate,
                PaymentMethod = x.PaymentMethod,
                TransactionId = x.TransactionId,
                Status = x.Status
            }).ToList();
        }


        // =========================
        // REJECTION REPORT
        // =========================

        public async Task<List<RejectionReportDto>> GetRejectionReportsAsync()
        {
            var loans = await _repository.GetRejectionReportsAsync();

            return loans.Select(x => new RejectionReportDto
            {
                LoanApplicationId = x.LoanApplicationId,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer?.FullName ?? string.Empty,
                LoanProductName = x.LoanProduct?.ProductName ?? string.Empty,
                RequestedAmount = x.RequestedAmount,
                Status = x.Status,
                ApplicationDate = x.ApplicationDate,
                RejectionReason = x.RejectionReason ?? string.Empty
            }).ToList();
        }


        // =========================
        // OVERDUE REPORT
        // =========================

        public async Task<List<OverdueReportDto>> GetOverdueReportsAsync()
        {
            var repayments = await _repository.GetOverdueReportsAsync();

            var today = DateTime.UtcNow;

            return repayments.Select(x => new OverdueReportDto
            {
                LoanRepaymentId = x.LoanRepaymentId,
                LoanApplicationId = x.LoanApplicationId,
                InstallmentNumber = x.InstallmentNumber,
                DueDate = x.DueDate,
                EMIAmount = x.EMIAmount,
                PaidAmount = x.PaidAmount,
                OutstandingAmount = x.EMIAmount - x.PaidAmount,
                DaysOverdue = Math.Max(
                    0,
                    (today.Date - x.DueDate.Date).Days),
                Status = x.Status
            }).ToList();
        }


        // =========================
        // CUSTOMER STATEMENT
        // =========================

        public async Task<CustomerStatementDto?> GetCustomerStatementAsync(
            int customerId)
        {
            var loan = await _repository
                .GetCustomerStatementAsync(customerId);

            if (loan == null)
                return null;

            var repayments = loan.LoanRepayments;

            return new CustomerStatementDto
            {
                CustomerId = loan.CustomerId,
                CustomerName = loan.Customer?.FullName ?? string.Empty,

                LoanApplicationId = loan.LoanApplicationId,

                LoanProductName =
                    loan.LoanProduct?.ProductName ?? string.Empty,

                LoanAmount = loan.RequestedAmount,

                LoanStatus = loan.Status,

                TotalEMIAmount =
                    repayments.Sum(x => x.EMIAmount),

                TotalPaidAmount =
                    repayments.Sum(x => x.PaidAmount),

                OutstandingAmount =
                    repayments.Sum(x =>
                        x.EMIAmount - x.PaidAmount),

                TotalInstallments =
                    repayments.Count,

                PaidInstallments =
                    repayments.Count(x =>
                        x.Status == "Paid"),

                PendingInstallments =
                    repayments.Count(x =>
                        x.Status != "Paid")
            };
        }


        // =========================
        // PAYMENT HISTORY
        // =========================

        public async Task<List<PaymentHistoryDto>> GetPaymentHistoryAsync(
            int loanApplicationId)
        {
            var payments = await _repository
                .GetPaymentHistoryAsync(loanApplicationId);

            return payments.Select(x => new PaymentHistoryDto
            {
                PaymentId = x.PaymentId,
                LoanApplicationId = x.LoanApplicationId,
                LoanRepaymentId = x.LoanRepaymentId,
                PaymentAmount = x.PaymentAmount,
                PaymentDate = x.PaymentDate,
                PaymentMethod = x.PaymentMethod,
                TransactionId = x.TransactionId,
                Status = x.Status,
                Remarks = x.Remarks
            }).ToList();
        }
    }
}
