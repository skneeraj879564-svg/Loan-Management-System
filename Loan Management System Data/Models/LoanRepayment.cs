using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class LoanRepayment
    {
        [Key]
        public int LoanRepaymentId { get; set; }

        public int LoanApplicationId { get; set; }

        [ForeignKey(nameof(LoanApplicationId))]
        public LoanApplication? LoanApplication { get; set; }

        public int InstallmentNumber { get; set; }

        public DateTime DueDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EMIAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string Status { get; set; } = "Pending";

    }
}
