using Loan_Management_System_Business.Dtos.Payment;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // =========================
        // GET ALL
        // GET: api/Payment
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments =
                await _paymentService.GetAllAsync();

            return Ok(payments);
        }

        // =========================
        // GET BY ID
        // GET: api/Payment/1
        // =========================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment =
                await _paymentService.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Payment not found."
                });
            }

            return Ok(payment);
        }

        // =========================
        // GET BY LOAN APPLICATION
        // GET: api/Payment/loan-application/6
        // =========================

        [HttpGet("loan-application/{loanApplicationId:int}")]
        public async Task<IActionResult>
            GetByLoanApplicationId(
                int loanApplicationId)
        {
            var payments =
                await _paymentService
                    .GetByLoanApplicationIdAsync(
                        loanApplicationId);

            return Ok(payments);
        }

        // =========================
        // GET BY LOAN REPAYMENT
        // GET: api/Payment/loan-repayment/1
        // =========================

        [HttpGet("loan-repayment/{loanRepaymentId:int}")]
        public async Task<IActionResult>
            GetByLoanRepaymentId(
                int loanRepaymentId)
        {
            var payments =
                await _paymentService
                    .GetByLoanRepaymentIdAsync(
                        loanRepaymentId);

            return Ok(payments);
        }

        // =========================
        // CREATE PAYMENT
        // POST: api/Payment
        // =========================

        [HttpPost]
        public async Task<IActionResult> Create(
            MakePaymentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment =
                await _paymentService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = payment.PaymentId },
                payment);
        }

        // =========================
        // UPDATE PAYMENT
        // PUT: api/Payment/1
        // =========================

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            MakePaymentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment =
                await _paymentService.UpdateAsync(
                    id,
                    model);

            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Payment not found."
                });
            }

            return Ok(payment);
        }

        // =========================
        // DELETE PAYMENT
        // DELETE: api/Payment/1
        // =========================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _paymentService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Payment not found."
                });
            }

            return Ok(new
            {
                message = "Payment deleted successfully."
            });
        }
    }
}
