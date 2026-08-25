using Loan_Management_System_Business.Dtos.Foreclosure;
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
    public class ForeclosureService : IForeclosureService
    {
        private readonly IForeclosureRepository _repository;
        private readonly ILoanRepository _loanRepository;

        public ForeclosureService(
            IForeclosureRepository repository,
            ILoanRepository loanRepository)
        {
            _repository = repository;
            _loanRepository = loanRepository;
        }

        // =========================
        // GET BY ID
        // =========================

        public async Task<ForeclosureDto?>
            GetByIdAsync(int foreclosureId)
        {
            var foreclosure =
                await _repository.GetByIdAsync(
                    foreclosureId);

            if (foreclosure == null)
            {
                return null;
            }

            return MapToDto(foreclosure);
        }

        // =========================
        // GET ALL
        // =========================

        public async Task<List<ForeclosureDto>>
            GetAllAsync()
        {
            var foreclosures =
                await _repository.GetAllAsync();

            return foreclosures
                .Select(MapToDto)
                .ToList();
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        public async Task<List<ForeclosureDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId)
        {
            var foreclosures =
                await _repository
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            return foreclosures
                .Select(MapToDto)
                .ToList();
        }

        // =========================
        // CREATE
        // =========================

        public async Task<ForeclosureDto>
            CreateAsync(ForeclosureDto model)
        {
            var foreclosure = new Foreclosure
            {
                LoanApplicationId =
                    model.LoanApplicationId,

                OutstandingPrincipal =
                    model.OutstandingPrincipal,

                InterestAmount =
                    model.InterestAmount,

                PenaltyAmount =
                    model.PenaltyAmount,

                ForeclosureCharges =
                    model.ForeclosureCharges,

                TotalAmount =
                    model.TotalAmount,

                ForeclosureDate =
                    model.ForeclosureDate,

                Reason =
                    model.Reason,

                Status =
                    model.Status,

                PaidDate =
                    model.PaidDate
            };

            var result =
                await _repository.AddAsync(
                    foreclosure);

            return MapToDto(result);
        }

        // =========================
        // UPDATE
        // =========================

        public async Task<ForeclosureDto?>
            UpdateAsync(
                int foreclosureId,
                ForeclosureDto model)
        {
            var foreclosure =
                await _repository.GetByIdAsync(
                    foreclosureId);

            if (foreclosure == null)
            {
                return null;
            }

            foreclosure.LoanApplicationId =
                model.LoanApplicationId;

            foreclosure.OutstandingPrincipal =
                model.OutstandingPrincipal;

            foreclosure.InterestAmount =
                model.InterestAmount;

            foreclosure.PenaltyAmount =
                model.PenaltyAmount;

            foreclosure.ForeclosureCharges =
                model.ForeclosureCharges;

            foreclosure.TotalAmount =
                model.TotalAmount;

            foreclosure.ForeclosureDate =
                model.ForeclosureDate;

            foreclosure.Reason =
                model.Reason;

            foreclosure.Status =
                model.Status;

            foreclosure.PaidDate =
                model.PaidDate;


            // =========================
            // CLOSE LOAN AFTER FORECLOSURE PAID
            // =========================

            if (model.Status == "Paid")
            {
                var loan =
                    await _loanRepository
                        .GetByLoanApplicationIdAsync(
                            model.LoanApplicationId);

                if (loan != null)
                {
                    loan.OutstandingAmount = 0;
                    loan.Status = "Closed";

                    await _loanRepository.UpdateAsync(
                        loan);
                }
            }


            // =========================
            // UPDATE FORECLOSURE
            // =========================

            var result =
                await _repository.UpdateAsync(
                    foreclosure);

            return MapToDto(result);
        }

        // =========================
        // DELETE
        // =========================

        public async Task<bool>
            DeleteAsync(int foreclosureId)
        {
            return await _repository.DeleteAsync(
                foreclosureId);
        }

        // =========================
        // MAPPING
        // =========================

        private static ForeclosureDto
            MapToDto(Foreclosure foreclosure)
        {
            return new ForeclosureDto
            {
                LoanApplicationId =
                    foreclosure.LoanApplicationId,

                OutstandingPrincipal =
                    foreclosure.OutstandingPrincipal,

                InterestAmount =
                    foreclosure.InterestAmount,

                PenaltyAmount =
                    foreclosure.PenaltyAmount,

                ForeclosureCharges =
                    foreclosure.ForeclosureCharges,

                TotalAmount =
                    foreclosure.TotalAmount,

                ForeclosureDate =
                    foreclosure.ForeclosureDate,

                Reason =
                    foreclosure.Reason,

                Status =
                    foreclosure.Status,

                PaidDate =
                    foreclosure.PaidDate
            };
        }
    }
}