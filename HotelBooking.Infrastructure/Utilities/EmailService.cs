using System.Net;
using System.Net.Mail;
using HotelBooking.Domain.Abstractions.Utilities;
using HotelBooking.Domain.Configurations;
using HotelBooking.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelBooking.Infrastructure.Utilities
{
    /// <inheritdoc cref="IEmailService"/>
    internal class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _fromAddress;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IOptions<EmailConfigurations> emailOptions, ILogger<EmailService> logger)
        {
            var emailConfig = emailOptions.Value;
            _fromAddress = new MailAddress(emailConfig.SenderEmail, emailConfig.SenderName);
            var fromPassword = emailConfig.SenderPassword;
            _smtpClient = new SmtpClient
            {
                Host = emailConfig.Host,
                Port = emailConfig.Port,
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
