using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanApplication
{
    public class LoanApplicationResponseDto
    {
        public int LoanApplicationId { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int LoanProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal RequestedAmount { get; set; }

        public int RequestedTenureMonths { get; set; }

        public string? Purpose { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string? RejectionReason { get; set; }

        public int? ApprovedByEmployeeId { get; set; }

        public string? ApprovedByEmployeeName { get; set; }

    }
}
