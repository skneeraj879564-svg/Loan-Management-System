using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        // Identity User ke saath relation
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        // Personal Information
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // Customer Information
        [MaxLength(20)]
        public string? CustomerCode { get; set; }

        [MaxLength(20)]
        public string? PanNumber { get; set; }

        [MaxLength(20)]
        public string? AadhaarNumber { get; set; }

        // Navigation Properties
        public Address? Address { get; set; }

        public EmploymentDetail? EmploymentDetail { get; set; }

        public BankAccount? BankAccount { get; set; }

        public Nominee? Nominee { get; set; }

        public ICollection<KycDocument> KycDocuments { get; set; }
            = new List<KycDocument>();
    }
}
