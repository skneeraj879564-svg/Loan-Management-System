using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.Foreclosure
{
    public class ForeclosureDto
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OutstandingPrincipal { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal InterestAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal PenaltyAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ForeclosureCharges { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime ForeclosureDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime? PaidDate { get; set; }
    }
}
