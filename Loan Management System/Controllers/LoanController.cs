using Loan_Management_System_Business.Dtos.Loan;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoanController(
            ILoanService service)
        {
            _service = service;
        }


        // =====================================================
        // GET ALL LOANS
        // Admin / LoanOfficer / CollectionOfficer
        // =====================================================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var loans =
                await _service.GetAllAsync();

            return Ok(loans);
        }


        // =====================================================
        // GET MY LOANS
        // Customer
        // =====================================================

        [HttpGet("my-loans")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyLoans()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new
                {
                    message = "User is not authenticated."
                });
            }

            return Ok(new
            {
                message = "Customer loans endpoint."
            });
        }


        // =====================================================
        // GET LOAN BY ID
        // Admin / LoanOfficer / CollectionOfficer
        // =====================================================

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var loan =
                await _service.GetByIdAsync(id);

            if (loan == null)
            {
                return NotFound(new
                {
                    message = "Loan not found."
                });
            }

            return Ok(loan);
        }


        // =====================================================
        // GET LOAN BY APPLICATION
        // Admin / LoanOfficer / CollectionOfficer
        // =====================================================

        [HttpGet("application/{loanApplicationId:int}")]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult>
            GetByLoanApplicationId(
                int loanApplicationId)
        {
            var loan =
                await _service
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            if (loan == null)
            {
                return NotFound(new
                {
                    message =
                        "Loan not found for this loan application."
                });
            }

            return Ok(loan);
        }


        // =====================================================
        // CREATE LOAN
        // Admin / LoanOfficer
        // =====================================================

        [HttpPost]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Create(
            CreateLoanDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loan =
                await _service.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = loan.LoanId },
                loan);
        }


        // =====================================================
        // UPDATE LOAN
        // Admin / LoanOfficer
        // =====================================================

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Update(
            int id,
            UpdateLoanDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loan =
                await _service.UpdateAsync(
                    id,
                    model);

            if (loan == null)
            {
                return NotFound(new
                {
                    message = "Loan not found."
                });
            }

            return Ok(loan);
        }


        // =====================================================
        // DELETE LOAN
        // Admin only
        // =====================================================

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Loan not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan deleted successfully."
            });
        }
    }
}