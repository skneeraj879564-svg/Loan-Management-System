namespace Loan_Management_System_Business.Dtos.EmailSetting
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;

        public int SmtpPort { get; set; }

        public string SenderEmail { get; set; } = string.Empty;

        public string SenderPassword { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;
    }
}