using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.VerificationHistory
{
    public class UpdateVerificationHistoryDto
    {
        [Required]
        [MaxLength(50)]
        public string VerificationType { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Remarks { get; set; }

        public int? CreditScore { get; set; }
    }
}
