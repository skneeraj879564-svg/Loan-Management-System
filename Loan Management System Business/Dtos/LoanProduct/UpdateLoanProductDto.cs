using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.LoanProduct
{
    public class UpdateLoanProductDto
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MinimumAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MaximumAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal InterestRate { get; set; }

        [Required]
        [Range(1, 360)]
        public int MinimumTenureMonths { get; set; }

        [Required]
        [Range(1, 360)]
        public int MaximumTenureMonths { get; set; }

        public bool IsActive { get; set; }

    }
}
