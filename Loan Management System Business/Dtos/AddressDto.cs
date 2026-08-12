using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Dtos
{
    public class AddressDto
    {
        [Required]
        [MaxLength(200)]
        public string AddressLine { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string PinCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? AddressType { get; set; }
    }
}
