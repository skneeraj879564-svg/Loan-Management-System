using Loan_Management_System_Business.Dtos.LoanProduct;
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
    public class LoanProductService:ILoanProductService
    {
        private readonly ILoanProductRepository _loanProductRepository;

        public LoanProductService(
            ILoanProductRepository loanProductRepository)
        {
            _loanProductRepository = loanProductRepository;
        }


        // =========================
        // GET BY ID
        // =========================

        public async Task<LoanProductResponseDto?> GetByIdAsync(
            int loanProductId)
        {
            var loanProduct =
                await _loanProductRepository
                    .GetByIdAsync(loanProductId);

            if (loanProduct == null)
            {
                return null;
            }

            return MapToResponse(loanProduct);
        }


        // =========================
        // GET ALL
        // =========================

        public async Task<List<LoanProductResponseDto>> GetAllAsync()
        {
            var loanProducts =
                await _loanProductRepository.GetAllAsync();

            return loanProducts
                .Select(MapToResponse)
                .ToList();
        }


        // =========================
        // CREATE
        // =========================

        public async Task<LoanProductResponseDto> CreateAsync(
            CreateLoanProductDto model)
        {
            var loanProduct = new LoanProduct
            {
                ProductName = model.ProductName,
                Description = model.Description,
                MinimumAmount = model.MinimumAmount,
                MaximumAmount = model.MaximumAmount,
                InterestRate = model.InterestRate,
                MinimumTenureMonths =
                    model.MinimumTenureMonths,
                MaximumTenureMonths =
                    model.MaximumTenureMonths,
                IsActive = model.IsActive
            };

            var result =
                await _loanProductRepository
                    .AddAsync(loanProduct);

            return MapToResponse(result);
        }


        // =========================
        // UPDATE
        // =========================

        public async Task<LoanProductResponseDto?> UpdateAsync(
            int loanProductId,
            UpdateLoanProductDto model)
        {
            var loanProduct =
                await _loanProductRepository
                    .GetByIdAsync(loanProductId);

            if (loanProduct == null)
            {
                return null;
            }

            loanProduct.ProductName =
                model.ProductName;

            loanProduct.Description =
                model.Description;

            loanProduct.MinimumAmount =
                model.MinimumAmount;

            loanProduct.MaximumAmount =
                model.MaximumAmount;

            loanProduct.InterestRate =
                model.InterestRate;

            loanProduct.MinimumTenureMonths =
                model.MinimumTenureMonths;

            loanProduct.MaximumTenureMonths =
                model.MaximumTenureMonths;

            loanProduct.IsActive =
                model.IsActive;

            var result =
                await _loanProductRepository
                    .UpdateAsync(loanProduct);

            return MapToResponse(result);
        }


        // =========================
        // DELETE
        // =========================

        public async Task<bool> DeleteAsync(
            int loanProductId)
        {
            return await _loanProductRepository
                .DeleteAsync(loanProductId);
        }


        // =========================
        // MAPPING
        // =========================

        private static LoanProductResponseDto
            MapToResponse(LoanProduct loanProduct)
        {
            return new LoanProductResponseDto
            {
                LoanProductId =
                    loanProduct.LoanProductId,

                ProductName =
                    loanProduct.ProductName,

                Description =
                    loanProduct.Description,

                MinimumAmount =
                    loanProduct.MinimumAmount,

                MaximumAmount =
                    loanProduct.MaximumAmount,

                InterestRate =
                    loanProduct.InterestRate,

                MinimumTenureMonths =
                    loanProduct.MinimumTenureMonths,

                MaximumTenureMonths =
                    loanProduct.MaximumTenureMonths,

                IsActive =
                    loanProduct.IsActive
            };
        }
    }
}
