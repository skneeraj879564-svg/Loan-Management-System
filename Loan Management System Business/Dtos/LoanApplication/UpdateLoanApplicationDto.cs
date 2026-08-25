using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanApplication
{
    public class UpdateLoanApplicationDto
    {
        [Required]
        [Range(1, double.MaxValue)]
        public decimal RequestedAmount { get; set; }

        [Required]
        [Range(1, 600)]
        public int RequestedTenureMonths { get; set; }

        public string? Purpose { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string? RejectionReason { get; set; }

        public int? ApprovedByEmployeeId { get; set; }

        public DateTime? ApprovalDate { get; set; }

    }
}
