using Loan_Management_System_Business.Dtos;
using Loan_Management_System_Business.Interfaces;
using Loan_Management_System_Data.Models;
using Loan_Management_System_Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Loan_Management_System_Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IAuthRepository authRepository,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        // =========================
        // REGISTER
        // =========================
        public async Task<string> RegisterAsync(RegisterDto model)
        {
            // Check email already exists
            var existingUser =
                await _authRepository.GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return "Email already registered.";
            }

            // Create ApplicationUser
            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.Email,
                Email = model.Email
            };

            // Create user using Identity
            var result = await _userManager.CreateAsync(
                user,
                model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                return errors;
            }

            // Assign default Customer role
            var roleResult = await _userManager.AddToRoleAsync(
                user,
                "Customer");

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(x => x.Description));

                return errors;
            }

            return "Registration successful.";
        }


        // =========================
        // LOGIN
        // =========================
        public async Task<LoginResponseDto?> LoginAsync(LoginDto model)
        {
            // Find user by email
            var user =
                await _authRepository.GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            // Check password
            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    model.Password);

            if (!passwordValid)
            {
                return null;
            }

            // Get user roles
            var roles =
                await _userManager.GetRolesAsync(user);

            var role =
                roles.FirstOrDefault() ?? "Customer";

            // Generate JWT Token
            var token =
                await _jwtService.GenerateTokenAsync(user);

            // Return Login Response
            return new LoginResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Role = role,
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            };
        }


        // =========================
        // FORGOT PASSWORD
        // =========================
        public async Task<string> ForgotPasswordAsync(
            ForgotPasswordDto model)
        {
            var user =
                await _authRepository.GetUserByEmailAsync(
                    model.Email);

            if (user == null)
            {
                return "If this email is registered, password reset instructions will be sent.";
            }

            // Generate password reset token
            var token =
                await _userManager.GeneratePasswordResetTokenAsync(
                    user);

            // Email service baad me add karenge
            return token;
        }


        // =========================
        // CHANGE PASSWORD
        // =========================
        public async Task<string> ChangePasswordAsync(
            string userId,
            ChangePasswordDto model)
        {
            // Find user
            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "User not found.";
            }

            // Change password
            var result =
                await _userManager.ChangePasswordAsync(
                    user,
                    model.CurrentPassword,
                    model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                return errors;
            }

            return "Password changed successfully.";
        }
    }
}