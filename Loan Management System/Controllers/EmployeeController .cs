using Loan_Management_System_Business.Dtos;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // ==========================================
        // GET ALL EMPLOYEES
        // GET: api/Employee
        // ==========================================

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var employees =
                await _employeeService.GetAllAsync();

            return Ok(employees);
        }


        // ==========================================
        // GET EMPLOYEE BY ID
        // GET: api/Employee/1
        // ==========================================

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee =
                await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = "Employee not found."
                });
            }

            return Ok(employee);
        }


        // ==========================================
        // GET MY PROFILE
        // GET: api/Employee/my-profile
        // ==========================================

        [HttpGet("my-profile")]
        [Authorize(Roles = "Admin,LoanOfficer,CollectionOfficer")]
        public async Task<IActionResult> GetMyProfile()
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

            var employee =
                await _employeeService
                    .GetMyProfileAsync(userId);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = "Employee profile not found."
                });
            }

            return Ok(employee);
        }


        // ==========================================
        // CREATE EMPLOYEE
        // POST: api/Employee
        // ==========================================

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateEmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee =
                await _employeeService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = employee.EmployeeId },
                employee);
        }


        // ==========================================
        // UPDATE EMPLOYEE
        // PUT: api/Employee/1
        // ==========================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            CreateEmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee =
                await _employeeService
                    .UpdateAsync(id, model);

            if (employee == null)
            {
                return NotFound(new
                {
                    message = "Employee not found."
                });
            }

            return Ok(employee);
        }


        // ==========================================
        // DELETE EMPLOYEE
        // DELETE: api/Employee/1
        // ==========================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Employee not found."
                });
            }

            return Ok(new
            {
                message = "Employee deleted successfully."
            });
        }
    }
}
