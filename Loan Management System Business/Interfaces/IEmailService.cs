using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System_Business.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(
          string toEmail,
          string subject,
          string body);
    }
}
