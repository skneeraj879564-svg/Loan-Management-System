using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class EmployeeProfile
    {
        // Primary Key
        public int EmployeeId { get; set; }

        // ApplicationUser se relation
        [Required]
        public string UserId { get; set; } = string.Empty;

        // Employee basic information
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // Employee information
        [Required]
        [MaxLength(50)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Designation { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Department { get; set; }

        // Branch information
        public int? BranchId { get; set; }

        // Joining information
        public DateTime? JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Salary
        public decimal? Salary { get; set; }

        // Navigation properties
        public ApplicationUser? User { get; set; }

        public Branch? Branch { get; set; }
    }
}
