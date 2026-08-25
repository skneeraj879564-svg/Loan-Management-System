using Loan_Management_System_Business.Dtos.LoanApplication;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;

namespace Loan_Management_System_Business.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _repository;
        private readonly ILoanRepaymentRepository _loanRepaymentRepository;

        public LoanApplicationService(
            ILoanApplicationRepository repository,
            ILoanRepaymentRepository loanRepaymentRepository)
        {
            _repository = repository;
            _loanRepaymentRepository = loanRepaymentRepository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanApplicationResponseDto?> GetByIdAsync(
            int loanApplicationId)
        {
            var application =
                await _repository.GetByIdAsync(loanApplicationId);

            if (application == null)
            {
                return null;
            }

            return MapToResponse(application);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanApplicationResponseDto>> GetAllAsync()
        {
            var applications =
                await _repository.GetAllAsync();

            return applications
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // GET BY CUSTOMER
        // =========================

        public async Task<List<LoanApplicationResponseDto>>
            GetByCustomerIdAsync(int customerId)
        {
            var applications =
                await _repository.GetByCustomerIdAsync(customerId);

            return applications
                .Select(MapToResponse)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public async Task<LoanApplicationResponseDto> CreateAsync(
            CreateLoanApplicationDto model)
        {
            var application = new LoanApplication
            {
                CustomerId = model.CustomerId,
                LoanProductId = model.LoanProductId,
                RequestedAmount = model.RequestedAmount,
                RequestedTenureMonths = model.RequestedTenureMonths,
                Purpose = model.Purpose,
                Status = "Pending",
                ApplicationDate = DateTime.UtcNow
            };

            var result =
                await _repository.AddAsync(application);

            return MapToResponse(result);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<LoanApplicationResponseDto?>
            UpdateAsync(
                int loanApplicationId,
                UpdateLoanApplicationDto model)
        {
            var application =
                await _repository.GetByIdAsync(
                    loanApplicationId);

            if (application == null)
            {
                return null;
            }

            application.RequestedAmount =
                model.RequestedAmount;

            application.RequestedTenureMonths =
                model.RequestedTenureMonths;

            application.Purpose =
                model.Purpose;

            application.Status =
                model.Status;

            application.RejectionReason =
                model.RejectionReason;

            application.ApprovedByEmployeeId =
                model.ApprovedByEmployeeId;

            application.ApprovalDate =
                model.ApprovalDate;

            var result =
                await _repository.UpdateAsync(application);

            return MapToResponse(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanApplicationId)
        {
            return await _repository.DeleteAsync(
                loanApplicationId);
        }

        // =========================
        // APPROVE + GENERATE EMI
        // =========================

        public async Task<bool> ApproveAsync(
            int loanApplicationId,
            int approvedByEmployeeId)
        {
            // Get application with LoanProduct
            var application =
                await _repository.GetByIdAsync(
                    loanApplicationId);

            if (application == null)
            {
                return false;
            }

            // Already approved?
            if (application.Status == "Approved")
            {
                return false;
            }

            // Loan product check
            if (application.LoanProduct == null)
            {
                return false;
            }

            // =========================
            // APPROVE APPLICATION
            // =========================

            application.Status = "Approved";
            application.ApprovalDate = DateTime.UtcNow;
            application.ApprovedByEmployeeId =
                approvedByEmployeeId;
            application.RejectionReason = null;

            await _repository.UpdateAsync(application);

            // =========================
            // CHECK EXISTING EMI SCHEDULE
            // =========================

            var existingRepayments =
                await _loanRepaymentRepository
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            if (existingRepayments.Any())
            {
                return true;
            }

            // =========================
            // LOAN DETAILS
            // =========================

            decimal principal =
                application.RequestedAmount;

            int tenureMonths =
                application.RequestedTenureMonths;

            decimal annualInterestRate =
                application.LoanProduct.InterestRate;

            // =========================
            // VALIDATION
            // =========================

            if (principal <= 0 ||
                tenureMonths <= 0 ||
                annualInterestRate < 0)
            {
                return false;
            }

            // =========================
            // MONTHLY INTEREST RATE
            // =========================

            decimal monthlyRate =
                annualInterestRate / 12 / 100;

            // =========================
            // EMI CALCULATION
            // =========================

            decimal emi;

            if (monthlyRate == 0)
            {
                emi =
                    principal / tenureMonths;
            }
            else
            {
                double p =
                    (double)principal;

                double r =
                    (double)monthlyRate;

                int n =
                    tenureMonths;

                double calculatedEmi =
                    p *
                    r *
                    Math.Pow(1 + r, n) /
                    (Math.Pow(1 + r, n) - 1);

                emi =
                    Math.Round(
                        (decimal)calculatedEmi,
                        2);
            }

            // =========================
            // GENERATE EMI SCHEDULE
            // =========================

            var repayments =
                new List<LoanRepayment>();

            decimal remainingPrincipal =
                principal;

            DateTime approvalDate =
                application.ApprovalDate.Value;

            for (int i = 1; i <= tenureMonths; i++)
            {
                // Interest for current month
                decimal interestAmount =
                    Math.Round(
                        remainingPrincipal *
                        monthlyRate,
                        2);

                decimal principalAmount =
                    Math.Round(
                        emi - interestAmount,
                        2);

                // Last installment adjustment
                if (i == tenureMonths)
                {
                    principalAmount =
                        remainingPrincipal;

                    emi =
                        Math.Round(
                            principalAmount +
                            interestAmount,
                            2);
                }

                remainingPrincipal =
                    Math.Round(
                        remainingPrincipal -
                        principalAmount,
                        2);

                var repayment =
                    new LoanRepayment
                    {
                        LoanApplicationId =
                            application.LoanApplicationId,

                        InstallmentNumber =
                            i,

                        DueDate =
                            approvalDate.AddMonths(i),

                        EMIAmount =
                            emi,

                        PrincipalAmount =
                            principalAmount,

                        InterestAmount =
                            interestAmount,

                        PaidAmount =
                            0,

                        PaymentDate =
                            null,

                        Status =
                            "Pending"
                    };

                repayments.Add(repayment);
            }

            // =========================
            // SAVE COMPLETE SCHEDULE
            // =========================

            await _loanRepaymentRepository
                .AddRangeAsync(repayments);

            return true;
        }

        // =========================
        // REJECT
        // =========================

        public async Task<bool> RejectAsync(
            int loanApplicationId,
            int rejectedByEmployeeId,
            string rejectionReason)
        {
            return await _repository.RejectAsync(
                loanApplicationId,
                rejectedByEmployeeId,
                rejectionReason);
        }

        // =========================
        // MAPPING
        // =========================

        private static LoanApplicationResponseDto
            MapToResponse(LoanApplication application)
        {
            return new LoanApplicationResponseDto
            {
                LoanApplicationId =
                    application.LoanApplicationId,

                CustomerId =
                    application.CustomerId,

                CustomerName =
                    application.Customer?.FullName,

                LoanProductId =
                    application.LoanProductId,

                ProductName =
                    application.LoanProduct?.ProductName,

                RequestedAmount =
                    application.RequestedAmount,

                RequestedTenureMonths =
                    application.RequestedTenureMonths,

                Purpose =
                    application.Purpose,

                Status =
                    application.Status,

                ApplicationDate =
                    application.ApplicationDate,

                ApprovalDate =
                    application.ApprovalDate,

                RejectionReason =
                    application.RejectionReason,

                ApprovedByEmployeeId =
                    application.ApprovedByEmployeeId,

                ApprovedByEmployeeName =
                    application.ApprovedByEmployee?.FullName
            };
        }
    }
}