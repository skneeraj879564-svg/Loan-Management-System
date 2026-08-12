using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos
{
    public class EmploymentDetailDto
    {
        [Required]
        [MaxLength(100)]
        public string EmploymentType { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? CompanyName { get; set; }

        [MaxLength(100)]
        public string? Designation { get; set; }

        [Range(0, 100000000)]
        public decimal? MonthlyIncome { get; set; }

        [Range(0, 1000000000)]
        public decimal? AnnualIncome { get; set; }

        [Range(0, 100)]
        public int? TotalExperienceYears { get; set; }

        [Range(0, 100)]
        public int? CurrentJobExperienceYears { get; set; }

        [MaxLength(200)]
        public string? CompanyAddress { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? OfficePhoneNumber { get; set; }

        public DateTime? JoiningDate { get; set; }
    }
}
