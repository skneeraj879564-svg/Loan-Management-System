using Loan_Management_System_Business.Dtos.LoanApplication;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _service;
        private readonly ICustomerService _customerService;

        public LoanApplicationController(
            ILoanApplicationService service,
            ICustomerService customerService)
        {
            _service = service;
            _customerService = customerService;
        }


        // =====================================================
        // GET ALL APPLICATIONS
        // ADMIN + LOAN OFFICER ONLY
        // =====================================================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var applications =
                await _service.GetAllAsync();

            return Ok(applications);
        }


        // =====================================================
        // GET APPLICATION BY ID
        // ADMIN / LOAN OFFICER → ANY APPLICATION
        // CUSTOMER → OWN APPLICATION ONLY
        // =====================================================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application =
                await _service.GetByIdAsync(id);

            if (application == null)
            {
                return NotFound(new
                {
                    message = "Loan application not found."
                });
            }


            // ================================================
            // ADMIN / LOAN OFFICER
            // ================================================

            if (User.IsInRole("Admin") ||
                User.IsInRole("LoanOfficer"))
            {
                return Ok(application);
            }


            // ================================================
            // CUSTOMER
            // ================================================

            if (User.IsInRole("Customer"))
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

                var customer =
                    await _customerService
                        .GetByUserIdAsync(userId);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer profile not found."
                    });
                }

                // Customer can access ONLY own application
                if (application.CustomerId != customer.CustomerId)
                {
                    return Forbid();
                }

                return Ok(application);
            }


            return Forbid();
        }


        // =====================================================
        // GET APPLICATIONS BY CUSTOMER
        // ADMIN / LOAN OFFICER → ANY CUSTOMER
        // CUSTOMER → OWN APPLICATIONS ONLY
        // =====================================================

        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetByCustomerId(
            int customerId)
        {
            // ================================================
            // ADMIN / LOAN OFFICER
            // ================================================

            if (User.IsInRole("Admin") ||
                User.IsInRole("LoanOfficer"))
            {
                var applications =
                    await _service
                        .GetByCustomerIdAsync(customerId);

                return Ok(applications);
            }


            // ================================================
            // CUSTOMER
            // ================================================

            if (User.IsInRole("Customer"))
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

                var customer =
                    await _customerService
                        .GetByUserIdAsync(userId);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer profile not found."
                    });
                }

                if (customer.CustomerId != customerId)
                {
                    return Forbid();
                }

                var applications =
                    await _service
                        .GetByCustomerIdAsync(customerId);

                return Ok(applications);
            }


            return Forbid();
        }


        // =====================================================
        // CREATE LOAN APPLICATION
        // ADMIN + CUSTOMER
        // =====================================================

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create(
            CreateLoanApplicationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // ================================================
            // CUSTOMER
            // ================================================

            if (User.IsInRole("Customer"))
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

                var customer =
                    await _customerService
                        .GetByUserIdAsync(userId);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer profile not found."
                    });
                }

                // IMPORTANT:
                // CustomerId should come from logged-in user,
                // not blindly from request.

                model.CustomerId = customer.CustomerId;
            }


            var application =
                await _service.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = application.LoanApplicationId
                },
                application);
        }


        // =====================================================
        // UPDATE LOAN APPLICATION
        // ADMIN + LOAN OFFICER ONLY
        // =====================================================

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Update(
            int id,
            UpdateLoanApplicationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application =
                await _service.UpdateAsync(
                    id,
                    model);

            if (application == null)
            {
                return NotFound(new
                {
                    message = "Loan application not found."
                });
            }

            return Ok(application);
        }


        // =====================================================
        // DELETE
        // ADMIN ONLY
        // =====================================================

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
                    message = "Loan application not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan application deleted successfully."
            });
        }


        // =====================================================
        // APPROVE
        // ADMIN + LOAN OFFICER
        // =====================================================

        [HttpPost("{id:int}/approve")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Approve(
            int id,
            ApproveLoanApplicationDto model)
        {
            var result =
                await _service.ApproveAsync(
                    id,
                    model.ApprovedByEmployeeId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Loan application not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan application approved successfully."
            });
        }


        // =====================================================
        // REJECT
        // ADMIN + LOAN OFFICER
        // =====================================================

        [HttpPost("{id:int}/reject")]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> Reject(
            int id,
            RejectLoanApplicationDto model)
        {
            var result =
                await _service.RejectAsync(
                    id,
                    model.RejectedByEmployeeId,
                    model.RejectionReason);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Loan application not found."
                });
            }

            return Ok(new
            {
                message =
                    "Loan application rejected successfully."
            });
        }
    }
}