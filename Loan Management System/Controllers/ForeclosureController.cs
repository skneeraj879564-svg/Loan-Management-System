using Loan_Management_System_Business.Dtos.Foreclosure;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ForeclosureController : ControllerBase
    {
        private readonly IForeclosureService _service;

        public ForeclosureController(
            IForeclosureService service)
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
                    message = "Foreclosure not found."
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
        // CREATE
        // =========================

        [HttpPost]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Create(
            [FromBody] ForeclosureDto model)
        {
            var result =
                await _service.CreateAsync(model);

            return Ok(result);
        }

        // =========================
        // UPDATE
        // =========================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ForeclosureDto model)
        {
            var result =
                await _service.UpdateAsync(id, model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Foreclosure not found."
                });
            }

            return Ok(result);
        }

        // =========================
        // DELETE
        // =========================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Foreclosure not found."
                });
            }

            return Ok(new
            {
                message = "Foreclosure deleted successfully."
            });
        }
    }
}
