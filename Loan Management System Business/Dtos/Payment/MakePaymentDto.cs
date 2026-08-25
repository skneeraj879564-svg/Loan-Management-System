using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Payment
{
    public class MakePaymentDto
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        public int LoanRepaymentId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PaymentAmount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        public string TransactionId { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}
