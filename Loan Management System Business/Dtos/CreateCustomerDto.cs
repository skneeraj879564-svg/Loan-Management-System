using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string? CustomerCode { get; set; }

        [MaxLength(20)]
        public string? PanNumber { get; set; }

        [MaxLength(20)]
        public string? AadhaarNumber { get; set; }

        // Address
        public AddressDto? Address { get; set; }

        // Employment
        public EmploymentDetailDto? EmploymentDetail { get; set; }

        // Bank Account
        public BankAccountDto? BankAccount { get; set; }

        // Nominee
        public NomineeDto? Nominee { get; set; }
    }
}
