using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Loan
{
    public class LoanResponseDto
    {
        public int LoanId { get; set; }

        public int LoanApplicationId { get; set; }

        public string LoanNumber { get; set; } = string.Empty;

        public decimal ApprovedAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int TenureMonths { get; set; }

        public decimal ProcessingFee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal OutstandingAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
