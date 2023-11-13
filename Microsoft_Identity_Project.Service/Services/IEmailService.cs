namespace Microsoft_Identity_Project.Service.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetPasswordEmailLink, string ToEmail);
        Task SendConfirmEmail(int confirmCode, string ToEmail);
    }
}
