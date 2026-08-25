using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Reports
{
    public class LoanReportDto
    {
        public int LoanApplicationId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string LoanProductName { get; set; } = string.Empty;

        public decimal RequestedAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime ApplicationDate { get; set; }
    }
}
