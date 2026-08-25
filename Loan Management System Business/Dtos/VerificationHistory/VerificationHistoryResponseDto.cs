using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos.VerificationHistory
{
    public class VerificationHistoryResponseDto
    {
        public int VerificationHistoryId { get; set; }

        public int LoanApplicationId { get; set; }

        public int EmployeeId { get; set; }

        public string VerificationType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? Remarks { get; set; }

        public int? CreditScore { get; set; }

        public DateTime VerificationDate { get; set; }
    }
}
