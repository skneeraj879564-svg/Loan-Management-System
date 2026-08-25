using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Reports
{
    public class CustomerStatementDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public int LoanApplicationId { get; set; }

        public string LoanProductName { get; set; } = string.Empty;

        public decimal LoanAmount { get; set; }

        public string LoanStatus { get; set; } = string.Empty;

        public decimal TotalEMIAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public decimal OutstandingAmount { get; set; }

        public int TotalInstallments { get; set; }

        public int PaidInstallments { get; set; }

        public int PendingInstallments { get; set; }
    }
}
