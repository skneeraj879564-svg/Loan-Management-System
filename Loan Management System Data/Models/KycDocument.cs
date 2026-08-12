using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class KycDocument
    {
        [Key]
        public int KycDocumentId { get; set; }

        // Customer relation
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        // Document Information
        [Required]
        [MaxLength(50)]
        public string DocumentType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string DocumentFilePath { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? VerificationStatus { get; set; }

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public DateTime? UploadedDate { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public bool IsVerified { get; set; } = false;
    }
}
