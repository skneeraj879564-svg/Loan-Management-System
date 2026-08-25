using Loan_Management_System_Business.Dtos.Authentication;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IAuthService
    {
        // REGISTER
        Task<string> RegisterAsync(
            RegisterDto model);

        // LOGIN
        Task<LoginResponseDto?> LoginAsync(
            LoginDto model);

        // FORGOT PASSWORD
        Task<string> ForgotPasswordAsync(
            ForgotPasswordDto model);

        // CHANGE PASSWORD
        Task<string> ChangePasswordAsync(
            string userId,
            ChangePasswordDto model);

        // RESET PASSWORD
        Task<string> ResetPasswordAsync(
            ResetPasswordDto model);
    }
}