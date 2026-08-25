using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Reports
{
    public class OverdueReportDto
    {
        public int LoanRepaymentId { get; set; }

        public int LoanApplicationId { get; set; }

        public int InstallmentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public decimal EMIAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal OutstandingAmount { get; set; }

        public int DaysOverdue { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
