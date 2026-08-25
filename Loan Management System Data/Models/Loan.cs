using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        // =========================
        // LOAN APPLICATION
        // =========================

        [Required]
        public int LoanApplicationId { get; set; }

        public LoanApplication LoanApplication { get; set; } = null!;


        // =========================
        // LOAN NUMBER
        // =========================

        [Required]
        [MaxLength(50)]
        public string LoanNumber { get; set; } = string.Empty;


        // =========================
        // APPROVED AMOUNT
        // =========================

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal ApprovedAmount { get; set; }


        // =========================
        // INTEREST RATE
        // =========================

        [Required]
        [Range(0, 100)]
        public decimal InterestRate { get; set; }


        // =========================
        // TENURE
        // =========================

        [Required]
        public int TenureMonths { get; set; }


        // =========================
        // PROCESSING FEE
        // =========================

        [Range(0, double.MaxValue)]
        public decimal ProcessingFee { get; set; }


        // =========================
        // LOAN START DATE
        // =========================

        [Required]
        public DateTime StartDate { get; set; }


        // =========================
        // LOAN END DATE
        // =========================

        [Required]
        public DateTime EndDate { get; set; }


        // =========================
        // OUTSTANDING AMOUNT
        // =========================

        [Range(0, double.MaxValue)]
        public decimal OutstandingAmount { get; set; }


        // =========================
        // LOAN STATUS
        // =========================

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Active";


        // =========================
        // CREATED DATE
        // =========================

        public DateTime CreatedDate { get; set; }
            = DateTime.UtcNow;
    }
}
