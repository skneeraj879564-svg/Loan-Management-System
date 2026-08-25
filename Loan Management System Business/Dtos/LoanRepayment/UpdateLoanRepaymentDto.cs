using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanRepayment
{
    public class UpdateLoanRepaymentDto
    {
        public DateTime DueDate { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal EMIAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PrincipalAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal InterestAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Status { get; set; } = "Pending";

    }
}
