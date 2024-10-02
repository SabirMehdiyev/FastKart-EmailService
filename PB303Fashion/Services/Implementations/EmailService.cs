using PB303Fashion.Services.Abstractions;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace PB303Fashion.Services.Implementations
{
    public class EmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string emailBody)
        {
            SmtpClient client = new SmtpClient(_configuration["EmailSettings:Smtp"], int.Parse(_configuration["EmailSettings:Port"]));
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["EmailSettings:Host"], _configuration["EmailSettings:Password"]);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration["EmailSettings:Host"]);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            mailMessage.Body = emailBody.ToString();

            await client.SendMailAsync(mailMessage);
        }
    }
}
