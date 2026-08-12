using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;

        public string? Department { get; set; }

        public int? BranchId { get; set; }

        public string? BranchName { get; set; }

        public DateTime? JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        public decimal? Salary { get; set; }

        public bool IsActive { get; set; }
    }
}
