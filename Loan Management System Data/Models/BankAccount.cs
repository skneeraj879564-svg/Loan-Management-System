using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }

        // Customer relation
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        // Bank Information
        [Required]
        [MaxLength(150)]
        public string BankName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string AccountHolderName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string IFSCCode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? AccountType { get; set; }

        [MaxLength(100)]
        public string? BranchName { get; set; }

        public bool IsPrimary { get; set; } = true;
    }
}
