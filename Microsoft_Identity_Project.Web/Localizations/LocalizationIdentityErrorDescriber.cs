using Microsoft.AspNetCore.Identity;

namespace Microsoft_Identity_Project.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DublicateUserName", Description = $"{userName} daha önce başka bir kullanıcı tarafından alındı!" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DublicateEmail", Description = $"{email} daha önce başka bir kullanıcı tarafından alındı!" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code ="PasswordTooShort", Description = $"Şifre en az 6 karakter içermeli!"};
        }
    }
}
