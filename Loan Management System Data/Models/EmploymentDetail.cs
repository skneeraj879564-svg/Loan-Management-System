using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Data.Models
{
    public class EmploymentDetail
    {
        [Key]
        public int EmploymentDetailId { get; set; }

        // Customer relation
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        // Employment Information
        [Required]
        [MaxLength(100)]
        public string EmploymentType { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? CompanyName { get; set; }

        [MaxLength(100)]
        public string? Designation { get; set; }

        public decimal? MonthlyIncome { get; set; }

        public decimal? AnnualIncome { get; set; }

        public int? TotalExperienceYears { get; set; }

        public int? CurrentJobExperienceYears { get; set; }

        [MaxLength(200)]
        public string? CompanyAddress { get; set; }

        [MaxLength(20)]
        public string? OfficePhoneNumber { get; set; }

        public DateTime? JoiningDate { get; set; }
    }
}
