using apiv2.Interfaces;
using apiv2.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace apiv2.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.Username));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        public async Task SendRegisterTemplateAsync(string toEmail, string fullName, string guardNumber, string associationName, string confirmUrl)
        {
            var template = File.ReadAllText("Templates/UserRegistered.html");
            template = template.Replace("{{FullName}}", fullName);
            template = template.Replace("{{GuardNumber}}", guardNumber);
            template = template.Replace("{{ConfirmUrl}}", confirmUrl);
            template = template.Replace("{{AssociationName}}", associationName);

            await SendAsync(toEmail, "Regisztrációs kérvény", template);
        }
    }
}