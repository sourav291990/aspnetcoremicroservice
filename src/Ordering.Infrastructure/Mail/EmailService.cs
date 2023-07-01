using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(EmailSettings emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            try
            {
                var sendgridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
                var response = await client.SendEmailAsync(sendgridMessage);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Email failed to send. Error message {ex.Message}");
                return false;
            }
            return false;
        }
    }
}
