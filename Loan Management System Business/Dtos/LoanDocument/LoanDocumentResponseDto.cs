//using System;

//namespace Loan_Management_System_Business.Dtos.LoanDocument
//{
//    public class LoanDocumentResponseDto
//    {
//        public int LoanDocumentId { get; set; }

//        public int LoanApplicationId { get; set; }

//        public string DocumentType { get; set; } = string.Empty;

//        public string DocumentName { get; set; } = string.Empty;

//        public string FilePath { get; set; } = string.Empty;

//        public string? StoredFileName { get; set; }

//        public string? ContentType { get; set; }

//        public long FileSize { get; set; }

//        public DateTime UploadedDate { get; set; }

//        public string VerificationStatus { get; set; } = string.Empty;

//        public string? VerificationRemarks { get; set; }

//        public int? VerifiedByEmployeeId { get; set; }

//        public DateTime? VerifiedDate { get; set; }
//    }
//}
using System;

namespace Loan_Management_System_Business.Dtos.LoanDocument
{
    public class LoanDocumentResponseDto
    {
        public int LoanDocumentId { get; set; }

        public int LoanApplicationId { get; set; }

        public string DocumentType { get; set; } = string.Empty;

        public string DocumentName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string? ContentType { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadedDate { get; set; }

        public string VerificationStatus { get; set; } = string.Empty;

        public string? VerificationRemarks { get; set; }

        public int? VerifiedByEmployeeId { get; set; }

        public DateTime? VerifiedDate { get; set; }
    }
}