using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanProduct
{
    public class LoanProductResponseDto
    {
        public int LoanProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal MinimumAmount { get; set; }

        public decimal MaximumAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int MinimumTenureMonths { get; set; }

        public int MaximumTenureMonths { get; set; }

        public bool IsActive { get; set; }
    }
}
