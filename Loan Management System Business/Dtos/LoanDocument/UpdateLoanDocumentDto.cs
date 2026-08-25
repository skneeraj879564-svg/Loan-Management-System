//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Loan_Management_System_Business.Dtos.LoanDocument
//{
//    public class UpdateLoanDocumentDto
//    {
//        [Required]
//        [MaxLength(100)]
//        public string DocumentType { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(200)]
//        public string DocumentName { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(500)]
//        public string FilePath { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(30)]
//        public string VerificationStatus { get; set; } = "Pending";

//        [MaxLength(500)]
//        public string? VerificationRemarks { get; set; }

//        public int? VerifiedByEmployeeId { get; set; }

//        public DateTime? VerifiedDate { get; set; }
//    }
//}
using System;
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System_Business.Dtos.LoanDocument
{
    public class UpdateLoanDocumentDto
    {
        [Required]
        [MaxLength(100)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string VerificationStatus { get; set; } = "Pending";

        [MaxLength(500)]
        public string? VerificationRemarks { get; set; }

        public int? VerifiedByEmployeeId { get; set; }

        public DateTime? VerifiedDate { get; set; }
    }
}
