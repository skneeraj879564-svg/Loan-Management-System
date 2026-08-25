using Loan_Management_System_Business.Dtos.Loan;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;

namespace Loan_Management_System_Business.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;

        public LoanService(
            ILoanRepository repository)
        {
            _repository = repository;
        }


        // =====================================================
        // GET BY ID
        // =====================================================

        public async Task<LoanResponseDto?> GetByIdAsync(
            int loanId)
        {
            var loan =
                await _repository.GetByIdAsync(loanId);

            if (loan == null)
            {
                return null;
            }

            return MapToResponse(loan);
        }


        // =====================================================
        // GET ALL
        // =====================================================

        public async Task<List<LoanResponseDto>> GetAllAsync()
        {
            var loans =
                await _repository.GetAllAsync();

            return loans
                .Select(MapToResponse)
                .ToList();
        }


        // =====================================================
        // GET BY LOAN APPLICATION
        // =====================================================

        public async Task<LoanResponseDto?>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var loan =
                await _repository
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            if (loan == null)
            {
                return null;
            }

            return MapToResponse(loan);
        }


        // =====================================================
        // GET BY CUSTOMER
        // =====================================================

        public async Task<List<LoanResponseDto>>
            GetByCustomerIdAsync(
                int customerId)
        {
            var loans =
                await _repository
                    .GetByCustomerIdAsync(
                        customerId);

            return loans
                .Select(MapToResponse)
                .ToList();
        }


        // =====================================================
        // CREATE
        // =====================================================

        public async Task<LoanResponseDto> CreateAsync(
            CreateLoanDto model)
        {
            var loan = new Loan
            {
                LoanApplicationId =
                    model.LoanApplicationId,

                LoanNumber =
                    model.LoanNumber,

                ApprovedAmount =
                    model.ApprovedAmount,

                InterestRate =
                    model.InterestRate,

                TenureMonths =
                    model.TenureMonths,

                ProcessingFee =
                    model.ProcessingFee,

                StartDate =
                    model.StartDate,

                EndDate =
                    model.EndDate,

                OutstandingAmount =
                    model.OutstandingAmount,

                Status =
                    model.Status,

                CreatedDate =
                    DateTime.UtcNow
            };

            var result =
                await _repository.AddAsync(loan);

            return MapToResponse(result);
        }


        // =====================================================
        // UPDATE
        // =====================================================

        public async Task<LoanResponseDto?>
            UpdateAsync(
                int loanId,
                UpdateLoanDto model)
        {
            var loan =
                await _repository.GetByIdAsync(
                    loanId);

            if (loan == null)
            {
                return null;
            }

            loan.ApprovedAmount =
                model.ApprovedAmount;

            loan.InterestRate =
                model.InterestRate;

            loan.TenureMonths =
                model.TenureMonths;

            loan.ProcessingFee =
                model.ProcessingFee;

            loan.StartDate =
                model.StartDate;

            loan.EndDate =
                model.EndDate;

            loan.OutstandingAmount =
                model.OutstandingAmount;

            loan.Status =
                model.Status;

            var result =
                await _repository.UpdateAsync(
                    loan);

            return MapToResponse(result);
        }


        // =====================================================
        // DELETE
        // =====================================================

        public async Task<bool> DeleteAsync(
            int loanId)
        {
            return await _repository
                .DeleteAsync(loanId);
        }


        // =====================================================
        // MAPPING
        // =====================================================

        private static LoanResponseDto MapToResponse(
            Loan loan)
        {
            return new LoanResponseDto
            {
                LoanId =
                    loan.LoanId,

                LoanApplicationId =
                    loan.LoanApplicationId,

                LoanNumber =
                    loan.LoanNumber,

                ApprovedAmount =
                    loan.ApprovedAmount,

                InterestRate =
                    loan.InterestRate,

                TenureMonths =
                    loan.TenureMonths,

                ProcessingFee =
                    loan.ProcessingFee,

                StartDate =
                    loan.StartDate,

                EndDate =
                    loan.EndDate,

                OutstandingAmount =
                    loan.OutstandingAmount,

                Status =
                    loan.Status,

                CreatedDate =
                    loan.CreatedDate
            };
        }
    }
}