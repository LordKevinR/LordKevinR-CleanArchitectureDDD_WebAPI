using CleanArchitecture.Application.Abstractions.Email;

namespace CleanArchitecture.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(Domain.Users.Email to, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}