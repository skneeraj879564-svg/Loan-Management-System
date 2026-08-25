using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Penalty
{
    public class PenaltyResponseDto
    {
        public int PenaltyId { get; set; }

        public int LoanApplicationId { get; set; }

        public int LoanRepaymentId { get; set; }

        public decimal PenaltyAmount { get; set; }

        public DateTime PenaltyDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime? PaidDate { get; set; }
    }
}
