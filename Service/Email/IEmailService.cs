using cotr.backend.Model;

namespace cotr.backend.Service.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
