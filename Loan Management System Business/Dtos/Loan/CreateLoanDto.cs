using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Loan
{
    public class CreateLoanDto
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoanNumber { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal ApprovedAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal InterestRate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TenureMonths { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ProcessingFee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal OutstandingAmount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active";
    }
}
