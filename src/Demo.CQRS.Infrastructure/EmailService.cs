using Demo.CQRS.BL.Contracts;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Demo.CQRS.Infrastructure
{

    public class EmailServiceConfiguration
    {
        public string ApiKey { get; set; }
        public string Sender { get; set; }
    }

    public class EmailService : IEmailService
    {
        private EmailServiceConfiguration _configuration;

        public EmailService(IOptions<EmailServiceConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public async Task SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_configuration.ApiKey);

            var sender = new EmailAddress(_configuration.Sender);
            var receiver = new EmailAddress(email.Receiver);
            var subject = email.Subject;
            var emailBody = email.Body;

            var sendGridMessage = MailHelper.CreateSingleEmail(sender, receiver, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            //if (!response.IsSuccessStatusCode)
            //    throw new IOException($"Failed to send SendGrid email, response: {response.StatusCode}.");
        }
    }
}
