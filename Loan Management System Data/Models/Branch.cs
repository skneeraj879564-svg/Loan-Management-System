using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class Branch
    {
        // Primary Key
        public int BranchId { get; set; }

        // Branch information
        [Required]
        [MaxLength(100)]
        public string BranchName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string PinCode { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        // Branch status
        public bool IsActive { get; set; } = true;

        // Navigation Property
        public ICollection<EmployeeProfile> Employees { get; set; }
            = new List<EmployeeProfile>();
    }
}
