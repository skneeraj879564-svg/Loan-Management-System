using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanApplication
{
    public class CreateLoanApplicationDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int LoanProductId { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal RequestedAmount { get; set; }

        [Required]
        [Range(1, 600)]
        public int RequestedTenureMonths { get; set; }

        public string? Purpose { get; set; }
    }
}
