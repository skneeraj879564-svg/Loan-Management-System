using Loan_Management_System_Business.Dtos.Authentication;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Loan_Management_System_Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IAuthRepository authRepository,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _authRepository = authRepository;
            _jwtService = jwtService;
            _emailService = emailService;
        }


        // =========================
        // REGISTER
        // =========================

        public async Task<string> RegisterAsync(
            RegisterDto model)
        {
            var existingUser =
                await _authRepository
                    .GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return "Email already registered.";
            }

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description));

                return errors;
            }

            var roleResult =
                await _userManager.AddToRoleAsync(
                    user,
                    "Customer");

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(
                        x => x.Description));

                return errors;
            }

            return "Registration successful.";
        }


        // =========================
        // LOGIN
        // =========================

        public async Task<LoginResponseDto?> LoginAsync(
            LoginDto model)
        {
            var user =
                await _authRepository
                    .GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password);

            if (!passwordValid)
            {
                return null;
            }

            var roles =
                await _userManager.GetRolesAsync(user);

            var role =
                roles.FirstOrDefault() ?? "Customer";

            var token =
                await _jwtService.GenerateTokenAsync(
                    user);

            return new LoginResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Role = role,
                Token = token,
                Expiration =
                    DateTime.UtcNow.AddMinutes(60)
            };
        }


        // =========================
        // FORGOT PASSWORD
        // =========================

        public async Task<string> ForgotPasswordAsync(
     ForgotPasswordDto model)
        {
            // 1. Find user by email
            var user =
                await _authRepository
                    .GetUserByEmailAsync(model.Email);

            // 2. Don't reveal whether email exists
            if (user == null)
            {
                return "If this email is registered, password reset instructions will be sent.";
            }

            // 3. Generate password reset token
            var token =
                await _userManager
                    .GeneratePasswordResetTokenAsync(user);

            // 4. Encode token so it can safely travel in URL
            var encodedToken =
                Uri.EscapeDataString(token);

            // 5. Create password reset link
            var resetLink =
                $"https://localhost:7256/api/Auth/reset-password" +
                $"?email={Uri.EscapeDataString(model.Email)}" +
                $"&token={encodedToken}";

            // 6. Email subject
            var subject =
                "Loan Management System - Password Reset";

            // 7. Email body
            var body = $@"
        <html>
        <body>
            <h2>Password Reset Request</h2>

            <p>Hello {user.FullName},</p>

            <p>
                We received a request to reset your
                Loan Management System password.
            </p>

            <p>
                Click the button below to reset your password:
            </p>

            <p>
                <a href='{resetLink}'
                   style='
                   display:inline-block;
                   padding:12px 20px;
                   background-color:#007bff;
                   color:white;
                   text-decoration:none;
                   border-radius:5px;'>
                   Reset Password
                </a>
            </p>

            <p>
                If you did not request a password reset,
                you can safely ignore this email.
            </p>

            <p>
                Regards,<br/>
                Loan Management System
            </p>
        </body>j
        </html>";

            // 8. Send email
            await _emailService.SendEmailAsync(
                model.Email,
                subject,
                body);

            // 9. Success response
            return "Password reset instructions have been sent to your email.";
        }

        // =========================
        // RESET PASSWORD
        // =========================

        public async Task<string> ResetPasswordAsync(
            ResetPasswordDto model)
        {
            // Find user by email
            var user =
                await _userManager
                    .FindByEmailAsync(model.Email);

            if (user == null)
            {
                return "Invalid password reset request.";
            }

            // Decode token
            var decodedToken =
                Uri.UnescapeDataString(model.Token);

            // Reset password
            var result =
                await _userManager.ResetPasswordAsync(
                    user,
                    decodedToken,
                    model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description));

                return errors;
            }

            return "Password reset successfully.";
        }


        // =========================
        // CHANGE PASSWORD
        // =========================

        public async Task<string> ChangePasswordAsync(
            string userId,
            ChangePasswordDto model)
        {
            var user =
                await _userManager
                    .FindByIdAsync(userId);

            if (user == null)
            {
                return "User not found.";
            }

            var result =
                await _userManager
                    .ChangePasswordAsync(
                        user,
                        model.CurrentPassword,
                        model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description));

                return errors;
            }

            return "Password changed successfully.";
        }
    }
}