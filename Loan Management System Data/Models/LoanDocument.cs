//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Loan_Management_System_Data.Models
//{
//    public class LoanDocument
//    {
//        [Key]
//        public int LoanDocumentId { get; set; }

//        // =========================
//        // LOAN APPLICATION
//        // =========================

//        [Required]
//        public int LoanApplicationId { get; set; }

//        public LoanApplication LoanApplication { get; set; } = null!;


//        // =========================
//        // DOCUMENT TYPE
//        // =========================

//        [Required]
//        [MaxLength(100)]
//        public string DocumentType { get; set; } = string.Empty;


//        // =========================
//        // DOCUMENT NAME
//        // =========================

//        [Required]
//        [MaxLength(200)]
//        public string DocumentName { get; set; } = string.Empty;


//        // =========================
//        // FILE PATH
//        // =========================

//        [Required]
//        [MaxLength(500)]
//        public string FilePath { get; set; } = string.Empty;


//        // =========================
//        // UPLOAD DATE
//        // =========================

//        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;


//        // =========================
//        // VERIFICATION STATUS
//        // =========================

//        [Required]
//        [MaxLength(30)]
//        public string VerificationStatus { get; set; } = "Pending";


//        // =========================
//        // VERIFICATION REMARKS
//        // =========================

//        [MaxLength(500)]
//        public string? VerificationRemarks { get; set; }


//        // =========================
//        // VERIFIED BY
//        // =========================

//        public int? VerifiedByEmployeeId { get; set; }

//        public EmployeeProfile? VerifiedByEmployee { get; set; }


//        // =========================
//        // VERIFIED DATE
//        // =========================

//        public DateTime? VerifiedDate { get; set; }
//    }
//}
using System;
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System_Data.Models
{
    public class LoanDocument
    {
        [Key]
        public int LoanDocumentId { get; set; }

        // =========================
        // LOAN APPLICATION
        // =========================

        [Required]
        public int LoanApplicationId { get; set; }

        public LoanApplication LoanApplication { get; set; } = null!;


        // =========================
        // DOCUMENT TYPE
        // =========================

        [Required]
        [MaxLength(100)]
        public string DocumentType { get; set; } = string.Empty;


        // =========================
        // DOCUMENT NAME
        // =========================

        [Required]
        [MaxLength(200)]
        public string DocumentName { get; set; } = string.Empty;


        // =========================
        // STORED FILE NAME
        // =========================

        [Required]
        [MaxLength(255)]
        public string StoredFileName { get; set; } = string.Empty;


        // =========================
        // FILE PATH
        // =========================

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;


        // =========================
        // CONTENT TYPE
        // =========================

        [MaxLength(100)]
        public string? ContentType { get; set; }


        // =========================
        // FILE SIZE
        // =========================

        public long FileSize { get; set; }


        // =========================
        // UPLOAD DATE
        // =========================

        public DateTime UploadedDate { get; set; }
            = DateTime.UtcNow;


        // =========================
        // VERIFICATION STATUS
        // =========================

        [Required]
        [MaxLength(30)]
        public string VerificationStatus { get; set; }
            = "Pending";


        // =========================
        // VERIFICATION REMARKS
        // =========================

        [MaxLength(500)]
        public string? VerificationRemarks { get; set; }


        // =========================
        // VERIFIED BY
        // =========================

        public int? VerifiedByEmployeeId { get; set; }

        public EmployeeProfile? VerifiedByEmployee { get; set; }


        // =========================
        // VERIFIED DATE
        // =========================

        public DateTime? VerifiedDate { get; set; }
    }
}