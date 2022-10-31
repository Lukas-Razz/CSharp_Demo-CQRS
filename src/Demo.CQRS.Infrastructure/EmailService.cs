using Demo.CQRS.BL.Contracts;
using FluentEmail.Core;

namespace Demo.CQRS.Infrastructure
{
    public class EmailService : IEmailService
    {
        private IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(BL.Contracts.Email email)
        {
            var response = await _fluentEmail
                .To(email.Receiver)
                .Subject(email.Subject)
                .Body(email.Body)
                .SendAsync();

            if (!response.Successful)
                throw new IOException($"Failed to send SendGrid email. {string.Join(Environment.NewLine, response.ErrorMessages)}");
        }
    }
}
