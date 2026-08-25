using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.EmiCalculator
{
    public class EmiCalculatorDto
    {
        [Required]
        [Range(1, 100000000)]
        public decimal LoanAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal AnnualInterestRate { get; set; }

        [Required]
        [Range(1, 360)]
        public int TenureMonths { get; set; }
    }
}
