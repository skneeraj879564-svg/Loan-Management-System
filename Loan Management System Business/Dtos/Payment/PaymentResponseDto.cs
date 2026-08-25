using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Payment
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }

        public int LoanApplicationId { get; set; }

        public int LoanRepaymentId { get; set; }

        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
