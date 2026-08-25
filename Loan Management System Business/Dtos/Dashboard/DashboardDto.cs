using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Dashboard
{
    public class DashboardDto
    {
        public int TotalCustomers { get; set; }

        public int TotalEmployees { get; set; }

        public int TotalLoanApplications { get; set; }

        public int PendingLoans { get; set; }

        public int ApprovedLoans { get; set; }

        public int RejectedLoans { get; set; }

        public int TotalLoanRepayments { get; set; }

        public int TotalPayments { get; set; }

        public decimal TotalPaymentAmount { get; set; }

        public decimal TotalOutstandingAmount { get; set; }
    }
}
