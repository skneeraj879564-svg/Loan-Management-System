using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class VerificationHistory
    {
        [Key]
        public int VerificationHistoryId { get; set; }

        // =========================
        // LOAN APPLICATION
        // =========================

        [Required]
        public int LoanApplicationId { get; set; }

        public LoanApplication LoanApplication { get; set; } = null!;


        // =========================
        // VERIFIED BY EMPLOYEE
        // =========================

        [Required]
        public int EmployeeId { get; set; }

        public EmployeeProfile Employee { get; set; } = null!;


        // =========================
        // VERIFICATION TYPE
        // =========================

        [Required]
        [MaxLength(50)]
        public string VerificationType { get; set; } = string.Empty;


        // =========================
        // VERIFICATION STATUS
        // =========================

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";


        // =========================
        // REMARKS
        // =========================

        [MaxLength(500)]
        public string? Remarks { get; set; }


        // =========================
        // CREDIT SCORE
        // =========================

        public int? CreditScore { get; set; }


        // =========================
        // VERIFICATION DATE
        // =========================

        public DateTime VerificationDate { get; set; }
            = DateTime.UtcNow;
    }
}
