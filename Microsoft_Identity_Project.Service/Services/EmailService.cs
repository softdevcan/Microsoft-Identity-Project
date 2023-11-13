using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Microsoft_Identity_Project.Core.OptionsModel;

namespace Microsoft_Identity_Project.Service.Services
{
    public class EmailService: IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail)
        {
            var smtpClient = new SmtpClient();

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = _emailSettings.Host;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = "Localhost | Password Reset Link";
            mailMessage.Body = @$"
                    <h4>Click on the link below to reset your password.</h4>
                    <p><a href='{resetPasswordEmailLink}'>password reset link</a></p>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendConfirmEmail(int confirmCode, string ToEmail)
        {
            var smtpClient = new SmtpClient();

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = _emailSettings.Host;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = "Localhost | Confirm Code Link";
            mailMessage.Body = $"Confirm Code: {confirmCode}";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
