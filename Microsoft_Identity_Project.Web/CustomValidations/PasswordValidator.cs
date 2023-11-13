using Microsoft.AspNetCore.Identity;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;

namespace Microsoft_Identity_Project.Web.CustomValidations
{
    public class PasswordValidator: IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new()
                {
                    Code = "PasswordContainUserName", Description = "Password field cannot contain username!"
                });
            }
            bool hasConsecutiveNumbers = false;
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (Char.IsDigit(password[i]) &&
                    Char.IsDigit(password[i + 1]) &&
                    Char.IsDigit(password[i + 2]) &&
                    Char.IsDigit(password[i + 3]))
                {
                    hasConsecutiveNumbers = true;
                    break;
                }
            }

            if (hasConsecutiveNumbers)
            {
                errors.Add(new() { Code = "PasswordContainConsecutiveNumbers", Description = "Password field cannot contain consecutive numbers!" });
            }
            if (errors.Any() )
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
