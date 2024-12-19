namespace CleanArchitecture.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(Domain.Users.Email to, string subject, string body);
    }
}