//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Loan_Management_System_Business.Dtos.LoanDocument
//{
//    public class CreateLoanDocumentDto
//    {
//        [Required]
//        public int LoanApplicationId { get; set; }

//        [Required]
//        [MaxLength(100)]
//        public string DocumentType { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(200)]
//        public string DocumentName { get; set; } = string.Empty;

//        [Required]
//        [MaxLength(500)]
//        public string FilePath { get; set; } = string.Empty;
//    }
//}
using System.ComponentModel.DataAnnotations;

namespace Loan_Management_System_Business.Dtos.LoanDocument
{
    public class CreateLoanDocumentDto
    {
        [Required]
        public int LoanApplicationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DocumentType { get; set; } = string.Empty;
    }
}