using Loan_Management_System_Business.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto?> GetByIdAsync(
            int paymentId);

        Task<List<PaymentResponseDto>> GetAllAsync();

        Task<List<PaymentResponseDto>>
            GetByLoanApplicationIdAsync(
                int loanApplicationId);

        Task<List<PaymentResponseDto>>
            GetByLoanRepaymentIdAsync(
                int loanRepaymentId);

        Task<PaymentResponseDto> CreateAsync(
            MakePaymentDto model);

        Task<PaymentResponseDto?> UpdateAsync(
            int paymentId,
            MakePaymentDto model);

        Task<bool> DeleteAsync(
            int paymentId);
    }
}
