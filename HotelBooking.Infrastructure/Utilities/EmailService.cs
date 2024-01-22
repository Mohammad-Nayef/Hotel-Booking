using System.Net.Mail;
using System.Net;
using HotelBooking.Domain.Abstractions.Utilities;
using Microsoft.Extensions.Configuration;
using HotelBooking.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Infrastructure.Utilities
{
    /// <inheritdoc cref="IEmailService"/>
    internal class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _fromAddress;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            var emailConfig = config.GetSection("EmailConfig");
            _fromAddress = new MailAddress(emailConfig["FromEmail"], emailConfig["FromName"]);
            var fromPassword = emailConfig["Password"];
            _smtpClient = new SmtpClient
            {
                Host = emailConfig["Host"],
                Port = int.Parse(emailConfig["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_fromAddress.Address, fromPassword)
            };
            _logger = logger;
        }

        public async Task SendAsync(EmailDTO email)
        {
            _logger.LogInformation("Sending email to {name}", email.ToName);
            var toAddress = new MailAddress(email.ToEmail, email.ToName);
            using (var message = new MailMessage(_fromAddress, toAddress)
            {
                Subject = email.Subject,
                Body = email.Body
            })
            {
                await _smtpClient.SendMailAsync(message);
            }
            _logger.LogInformation("Email sent to {name}", email.ToName);
        }
    }
}
