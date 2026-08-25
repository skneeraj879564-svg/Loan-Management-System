using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int LoanApplicationId { get; set; }

        [ForeignKey(nameof(LoanApplicationId))]
        public LoanApplication? LoanApplication { get; set; }

        public int LoanRepaymentId { get; set; }

        [ForeignKey(nameof(LoanRepaymentId))]
        public LoanRepayment? LoanRepayment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;

        public string Status { get; set; } = "Success";

        public string? Remarks { get; set; }
    }
}
