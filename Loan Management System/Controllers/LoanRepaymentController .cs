using Loan_Management_System_Business.Dtos.LoanRepayment;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly ILoanRepaymentService _service;

        public LoanRepaymentController(
            ILoanRepaymentService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // GET: api/LoanRepayment
        // =========================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var repayments = await _service.GetAllAsync();

            return Ok(repayments);
        }


        // =========================
        // GET BY ID
        // GET: api/LoanRepayment/1
        // =========================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var repayment =
                await _service.GetByIdAsync(id);

            if (repayment == null)
            {
                return NotFound(new
                {
                    message = "Loan repayment not found."
                });
            }

            return Ok(repayment);
        }


        // =========================
        // GET BY LOAN APPLICATION
        // GET:
        // api/LoanRepayment/application/6
        // =========================

        [HttpGet("application/{loanApplicationId:int}")]
        public async Task<IActionResult> GetByLoanApplicationId(
            int loanApplicationId)
        {
            var repayments =
                await _service.GetByLoanApplicationIdAsync(
                    loanApplicationId);

            return Ok(repayments);
        }


        // =========================
        // CREATE
        // POST: api/LoanRepayment
        // =========================

        [HttpPost]
        [Authorize(Roles = "Admin,CollectionOfficer")]
        public async Task<IActionResult> Create(
            CreateLoanRepaymentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repayment =
                await _service.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = repayment.LoanRepaymentId },
                repayment);
        }


        // =========================
        // UPDATE
        // PUT: api/LoanRepayment/1
        // =========================

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,CollectionOfficer")]
        public async Task<IActionResult> Update(
            int id,
            UpdateLoanRepaymentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var repayment =
                await _service.UpdateAsync(id, model);

            if (repayment == null)
            {
                return NotFound(new
                {
                    message = "Loan repayment not found."
                });
            }

            return Ok(repayment);
        }


        // =========================
        // DELETE
        // DELETE: api/LoanRepayment/1
        // =========================

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Loan repayment not found."
                });
            }

            return Ok(new
            {
                message = "Loan repayment deleted successfully."
            });
        }
    }
}
