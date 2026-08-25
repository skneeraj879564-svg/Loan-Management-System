using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanApplication
{
    public class RejectLoanApplicationDto
    {
        [Required]
        public string RejectionReason { get; set; } = string.Empty;

        public int RejectedByEmployeeId { get; set; }
    }
}
