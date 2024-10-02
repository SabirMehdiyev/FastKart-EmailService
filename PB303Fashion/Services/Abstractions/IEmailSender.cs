namespace PB303Fashion.Services.Abstractions;

public interface IEmailSender
{
    Task SendEmailAsync(string toEmail, string subject, string emailBody);
}
