using Loan_Management_System_Business.Dtos;
using Loan_Management_System_Business.Dtos.Authentication;
using Loan_Management_System_Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // ==========================================
        // REGISTER
        // POST: api/Auth/register
        // ==========================================

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(
            RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _authService.RegisterAsync(model);

            if (result != "Registration successful.")
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            return Ok(new
            {
                message = result
            });
        }


        // ==========================================
        // LOGIN
        // POST: api/Auth/login
        // ==========================================

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _authService.LoginAsync(model);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(result);
        }


        // ==========================================
        // FORGOT PASSWORD
        // POST: api/Auth/forgot-password
        // ==========================================

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(
            ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _authService.ForgotPasswordAsync(model);

            return Ok(new
            {
                message = result
            });
        }


        // ==========================================
        // RESET PASSWORD
        // POST: api/Auth/reset-password
        // ==========================================

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _authService.ResetPasswordAsync(model);

            if (result != "Password reset successfully.")
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            return Ok(new
            {
                message = result
            });
        }


        // ==========================================
        // CHANGE PASSWORD
        // POST: api/Auth/change-password
        // ==========================================

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // User ID JWT claim se milegi
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

            var result =
                await _authService.ChangePasswordAsync(
                    userId,
                    model);

            if (result != "Password changed successfully.")
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            return Ok(new
            {
                message = result
            });
        }
    }
}