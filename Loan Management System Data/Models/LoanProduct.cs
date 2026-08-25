using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class LoanProduct
    {
        [Key]
        public int LoanProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal MinimumAmount { get; set; }

        [Required]
        public decimal MaximumAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int MinimumTenureMonths { get; set; }

        [Required]
        public int MaximumTenureMonths { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
