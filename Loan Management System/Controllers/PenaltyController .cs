using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PenaltyController : ControllerBase
    {
        private readonly IPenaltyService _service;

        public PenaltyController(
            IPenaltyService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(result);
        }

        // =========================
        // GET BY ID
        // =========================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Penalty not found."
                });
            }

            return Ok(result);
        }

        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        [HttpGet("loan-application/{loanApplicationId}")]
        public async Task<IActionResult>
            GetByLoanApplicationId(
                int loanApplicationId)
        {
            var result =
                await _service
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            return Ok(result);
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // =========================

        [HttpGet("loan-repayment/{loanRepaymentId}")]
        public async Task<IActionResult>
            GetByLoanRepaymentId(
                int loanRepaymentId)
        {
            var result =
                await _service
                    .GetByLoanRepaymentIdAsync(
                        loanRepaymentId);

            return Ok(result);
        }

        // =========================
        // CREATE
        // =========================

        [HttpPost]
        public async Task<IActionResult> Create(
            int loanApplicationId,
            int loanRepaymentId,
            decimal penaltyAmount,
            DateTime penaltyDate,
            string reason)
        {
            var result =
                await _service.CreateAsync(
                    loanApplicationId,
                    loanRepaymentId,
                    penaltyAmount,
                    penaltyDate,
                    reason);

            return Ok(result);
        }

        // =========================
        // UPDATE
        // =========================

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            decimal penaltyAmount,
            DateTime penaltyDate,
            string reason,
            string status,
            DateTime? paidDate)
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    penaltyAmount,
                    penaltyDate,
                    reason,
                    status,
                    paidDate);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Penalty not found."
                });
            }

            return Ok(result);
        }

        // =========================
        // DELETE
        // =========================

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Penalty not found."
                });
            }

            return Ok(new
            {
                message = "Penalty deleted successfully."
            });
        }
    }
}
