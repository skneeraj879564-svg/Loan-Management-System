using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(
            IDashboardService service)
        {
            _service = service;
        } 

        // =========================
        // GET DASHBOARD
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result =
                await _service.GetDashboardAsync();

            return Ok(result);
        }
    }
}
