using Loan_Management_System_Business.Dtos.EmailSetting;
using Loan_Management_System_Business.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Loan_Management_System_Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(
            string toEmail,
            string subject,
            string body)
        {
            var smtpServer = _emailSettings.SmtpServer;

            var smtpPort = _emailSettings.SmtpPort;

            var senderEmail = _emailSettings.SenderEmail;

            var senderPassword = _emailSettings.SenderPassword;

            var senderName = _emailSettings.SenderName;

            using var message = new MailMessage();

            message.From = new MailAddress(
                senderEmail,
                senderName);

            message.To.Add(toEmail);

            message.Subject = subject;

            message.Body = body;

            message.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(
                smtpServer,
                smtpPort);

            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential(
                senderEmail,
                senderPassword);

            await smtpClient.SendMailAsync(message);
        }
    }
}