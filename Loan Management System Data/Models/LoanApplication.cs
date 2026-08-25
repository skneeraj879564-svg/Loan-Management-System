using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class LoanApplication
    {
        [Key]
        public int LoanApplicationId { get; set; }

        // =========================
        // CUSTOMER
        // =========================

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;


        // =========================
        // LOAN PRODUCT
        // =========================

        [Required]
        public int LoanProductId { get; set; }

        public LoanProduct LoanProduct { get; set; } = null!;


        // =========================
        // APPLICATION DETAILS
        // =========================

        [Required]
        public decimal RequestedAmount { get; set; }

        [Required]
        public int RequestedTenureMonths { get; set; }

        public string? Purpose { get; set; }


        // =========================
        // APPLICATION STATUS
        // =========================

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";


        // =========================
        // APPLICATION DATE
        // =========================

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;


        // =========================
        // APPROVAL DETAILS
        // =========================

        public DateTime? ApprovalDate { get; set; }

        public string? RejectionReason { get; set; }


        // =========================
        // LOAN OFFICER
        // =========================

        public int? ApprovedByEmployeeId { get; set; }

        public EmployeeProfile? ApprovedByEmployee { get; set; }
        public ICollection<LoanRepayment> LoanRepayments { get; set; }= new List<LoanRepayment>();
        public ICollection<LoanDocument> LoanDocuments { get; set; }= new List<LoanDocument>();
        public ICollection<VerificationHistory> VerificationHistories { get; set; } = new List<VerificationHistory>();
        public Loan? Loan { get; set; }
    }
}
