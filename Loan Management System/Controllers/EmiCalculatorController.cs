using Loan_Management_System_Business.Dtos.EmiCalculator;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmiCalculatorController : ControllerBase
    {
        private readonly IEmiCalculatorService _emiCalculatorService;

        public EmiCalculatorController(
            IEmiCalculatorService emiCalculatorService)
        {
            _emiCalculatorService = emiCalculatorService;
        }

        // =========================
        // CALCULATE EMI
        // POST: api/EmiCalculator/calculate
        // =========================

        [HttpPost("calculate")]
        public IActionResult Calculate(
            [FromBody] EmiCalculatorDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                _emiCalculatorService.CalculateEmi(model);

            return Ok(result);
        }
    }
}
