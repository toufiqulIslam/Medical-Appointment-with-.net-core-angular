using System.Net;
using System.Net.Mail;

namespace MedicalAppointmentAPI.Services {
    public class EmailService {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) { _config = config; }

        public async Task SendEmailAsync(string toEmail, string subject, string body, byte[]? attachment = null, string filename = "PrescriptionReport.pdf") {
            var smtpHost = _config["Smtp:Host"];
            var smtpPort = int.Parse(_config["Smtp:Port"]);
            var smtpUser = _config["Smtp:Email"];
            var smtpPass = _config["Smtp:Password"];

            using var client = new SmtpClient(smtpHost, smtpPort) {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            using var message = new MailMessage(smtpUser, toEmail, subject, body);
            if (attachment != null) {
                using var ms = new MemoryStream(attachment);
                message.Attachments.Add(new Attachment(ms, filename, "application/pdf"));
            }
            await client.SendMailAsync(message);
        }
    }
}