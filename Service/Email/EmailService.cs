using System.Net.Mail;
using System.Net;
using cotr.backend.Model;

namespace cotr.backend.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
                        _configuration["EmailCredentials:UserName"],
                        _configuration["EmailCredentials:Password"]
                    )
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailCredentials:UserName"] ?? throw new ApiException(500, "No se ha podido cargar la configuración del correo")),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(message.To);

            
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
