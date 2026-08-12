using Loan_Management_System_Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);

        Task<LoginResponseDto?> LoginAsync(LoginDto model);

        Task<string> ForgotPasswordAsync(ForgotPasswordDto model);

        Task<string> ChangePasswordAsync(
            string userId,
            ChangePasswordDto model);
    }
}
