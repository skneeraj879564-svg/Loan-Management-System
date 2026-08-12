using Microsoft.AspNetCore.Identity;
namespace Loan_Management_System_Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
