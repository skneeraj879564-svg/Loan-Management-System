using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.EmiCalculator
{
    public class EmiCalculatorResponseDto
    {
        public decimal LoanAmount { get; set; }

        public decimal AnnualInterestRate { get; set; }

        public int TenureMonths { get; set; }

        public decimal MonthlyEMI { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal TotalPayment { get; set; }
    }
}
