using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanRepayment
{
    public class CreateLoanRepaymentDto
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        public int InstallmentNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal EMIAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrincipalAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal InterestAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";
    }
}
