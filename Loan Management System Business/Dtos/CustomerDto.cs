using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? CustomerCode { get; set; }

        public string? PanNumber { get; set; }

        public string? AadhaarNumber { get; set; }
    }
}
