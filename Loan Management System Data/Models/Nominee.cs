using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class Nominee
    {
        [Key]
        public int NomineeId { get; set; }

        // Customer relation
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        // Nominee Information
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(20)]
        public string? AadhaarNumber { get; set; }
    }
}
