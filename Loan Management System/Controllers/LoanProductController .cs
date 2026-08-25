using Loan_Management_System_Business.Dtos.LoanProduct;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanProductController : ControllerBase
    {
        private readonly ILoanProductService _loanProductService;

        public LoanProductController(
            ILoanProductService loanProductService)
        {
            _loanProductService = loanProductService;
        }


        // =========================
        // GET ALL LOAN PRODUCTS
        // GET: api/LoanProduct
        // =========================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var loanProducts =
                await _loanProductService.GetAllAsync();

            return Ok(loanProducts);
        }


        // =========================
        // GET LOAN PRODUCT BY ID
        // GET: api/LoanProduct/1
        // =========================

        [HttpGet("{loanProductId:int}")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetById(
            int loanProductId)
        {
            var loanProduct =
                await _loanProductService
                    .GetByIdAsync(loanProductId);

            if (loanProduct == null)
            {
                return NotFound(new
                {
                    message = "Loan product not found."
                });
            }

            return Ok(loanProduct);
        }


        // =========================
        // CREATE LOAN PRODUCT
        // POST: api/LoanProduct
        // =========================

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateLoanProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loanProduct =
                await _loanProductService
                    .CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    loanProductId =
                        loanProduct.LoanProductId
                },
                loanProduct);
        }


        // =========================
        // UPDATE LOAN PRODUCT
        // PUT: api/LoanProduct/1
        // =========================

        [HttpPut("{loanProductId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int loanProductId,
            UpdateLoanProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loanProduct =
                await _loanProductService
                    .UpdateAsync(
                        loanProductId,
                        model);

            if (loanProduct == null)
            {
                return NotFound(new
                {
                    message = "Loan product not found."
                });
            }

            return Ok(loanProduct);
        }


        // =========================
        // DELETE LOAN PRODUCT
        // DELETE: api/LoanProduct/1
        // =========================

        [HttpDelete("{loanProductId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int loanProductId)
        {
            var deleted =
                await _loanProductService
                    .DeleteAsync(loanProductId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Loan product not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan product deleted successfully."
            });
        }

    }
}
