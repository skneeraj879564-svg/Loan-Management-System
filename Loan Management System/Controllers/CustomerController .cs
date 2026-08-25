using Loan_Management_System_Business.Dtos.Customer;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            ICustomerService customerService)
        {
            _customerService = customerService;
        }


        // =====================================================
        // GET CUSTOMER BY ID
        // =====================================================

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> GetById(int customerId)
        {
            // ================================================
            // ADMIN / LOAN OFFICER
            // Can view any customer
            // ================================================

            if (User.IsInRole("Admin") ||
                User.IsInRole("LoanOfficer"))
            {
                var customer =
                    await _customerService.GetByIdAsync(customerId);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });
                }

                return Ok(customer);
            }


            // ================================================
            // CUSTOMER
            // Can view ONLY own customer profile
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

                var ownCustomer =
                    await _customerService
                        .GetByUserIdAsync(userId);

                if (ownCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer profile not found."
                    });
                }

                if (ownCustomer.CustomerId != customerId)
                {
                    return Forbid();
                }

                return Ok(ownCustomer);
            }


            return Forbid();
        }


        // =====================================================
        // GET MY PROFILE
        // =====================================================

        [HttpGet("my-profile")]
        [Authorize(Roles = "Customer")]
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

            return Ok(customer);
        }


        // =====================================================
        // GET ALL CUSTOMERS
        // =====================================================

        [HttpGet]
        [Authorize(Roles = "Admin,LoanOfficer")]
        public async Task<IActionResult> GetAll()
        {
            var customers =
                await _customerService.GetAllAsync();

            return Ok(customers);
        }


        // =====================================================
        // CREATE CUSTOMER
        // =====================================================

        [HttpPost]
        [Authorize(Roles = "Admin,LoanOfficer,Customer")]
        public async Task<IActionResult> Create(
            CreateCustomerDto model)
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
                await _customerService.CreateAsync(
                    userId,
                    model);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    customerId = customer.CustomerId
                },
                customer);
        }


        // =====================================================
        // UPDATE CUSTOMER
        // =====================================================

        [HttpPut("{customerId:int}")]
        [Authorize(Roles = "Admin,LoanOfficer,Customer")]
        public async Task<IActionResult> Update(
            int customerId,
            UpdateCustomerDto model)
        {
            // ================================================
            // ADMIN / LOAN OFFICER
            // Can update any customer
            // ================================================

            if (User.IsInRole("Admin") ||
                User.IsInRole("LoanOfficer"))
            {
                var customer =
                    await _customerService.UpdateAsync(
                        customerId,
                        model);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });
                }

                return Ok(customer);
            }


            // ================================================
            // CUSTOMER
            // Can update ONLY own profile
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

                var ownCustomer =
                    await _customerService
                        .GetByUserIdAsync(userId);

                if (ownCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer profile not found."
                    });
                }

                if (ownCustomer.CustomerId != customerId)
                {
                    return Forbid();
                }

                var updatedCustomer =
                    await _customerService.UpdateAsync(
                        customerId,
                        model);

                if (updatedCustomer == null)
                {
                    return NotFound(new
                    {
                        message = "Customer not found."
                    });
                }

                return Ok(updatedCustomer);
            }


            return Forbid();
        }


        // =====================================================
        // DELETE CUSTOMER
        // =====================================================

        [HttpDelete("{customerId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            int customerId)
        {
            var deleted =
                await _customerService.DeleteAsync(
                    customerId);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Customer not found."
                });
            }

            return Ok(new
            {
                message = "Customer deleted successfully."
            });
        }
    }
}