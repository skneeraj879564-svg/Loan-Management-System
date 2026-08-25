using Loan_Management_System_Business.Dtos.Notification;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(
            INotificationService service)
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
                    message = "Notification not found."
                });
            }

            return Ok(result);
        }

        // =========================
        // GET BY USER ID
        // =========================

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(
            string userId)
        {
            var result =
                await _service.GetByUserIdAsync(userId);

            return Ok(result);
        }

        // =========================
        // CREATE
        // =========================

        [HttpPost]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Create(
            [FromBody] NotificationDto model)
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
            [FromBody] NotificationDto model)
        {
            var result =
                await _service.UpdateAsync(
                    id,
                    model);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Notification not found."
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
                    message = "Notification not found."
                });
            }

            return Ok(new
            {
                message =
                    "Notification deleted successfully."
            });
        }
    }
}
