using Loan_Management_System_Business.Dtos.VerificationHistory;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,LoanOfficer")]
    public class VerificationHistoryController : ControllerBase
    {
        private readonly IVerificationHistoryService _service;

        public VerificationHistoryController(
            IVerificationHistoryService service)
        {
            _service = service;
        }


        // =========================
        // GET ALL
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

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
                    message = "Verification history not found."
                });
            }

            return Ok(result);
        }


        // =========================
        // GET BY LOAN APPLICATION
        // =========================

        [HttpGet("application/{loanApplicationId}")]
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
        // CREATE
        // =========================

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody]
            CreateVerificationHistoryDto model)
        {
            var result =
                await _service.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = result.VerificationHistoryId
                },
                result);
        }


        // =========================
        // UPDATE
        // =========================

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody]
            UpdateVerificationHistoryDto model)
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Verification history not found."
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
                    message = "Verification history not found."
                });
            }

            return Ok(new
            {
                message =
                    "Verification history deleted successfully."
            });
        }
    }
}
