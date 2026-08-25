using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        // =========================
        // LOAN REPORT
        // =========================

        [HttpGet("loans")]
        public async Task<IActionResult> GetLoanReports()
        {
            var result = await _service.GetLoanReportsAsync();

            return Ok(result);
        }


        // =========================
        // COLLECTION REPORT
        // =========================

        [HttpGet("collections")]
        public async Task<IActionResult> GetCollectionReports()
        {
            var result =
                await _service.GetCollectionReportsAsync();

            return Ok(result);
        }


        // =========================
        // REJECTION REPORT
        // =========================

        [HttpGet("rejections")]
        public async Task<IActionResult> GetRejectionReports()
        {
            var result =
                await _service.GetRejectionReportsAsync();

            return Ok(result);
        }


        // =========================
        // OVERDUE REPORT
        // =========================

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueReports()
        {
            var result =
                await _service.GetOverdueReportsAsync();

            return Ok(result);
        }


        // =========================
        // CUSTOMER STATEMENT
        // =========================

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerStatement(
            int customerId)
        {
            var result =
                await _service.GetCustomerStatementAsync(customerId);

            if (result == null)
                return NotFound(new
                {
                    message = "Customer statement not found."
                });

            return Ok(result);
        }


        // =========================
        // PAYMENT HISTORY
        // =========================

        [HttpGet("payments/{loanApplicationId}")]
        public async Task<IActionResult> GetPaymentHistory(
            int loanApplicationId)
        {
            var result =
                await _service.GetPaymentHistoryAsync(
                    loanApplicationId);

            return Ok(result);
        }
    }
}
