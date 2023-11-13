using Microsoft.AspNetCore.Identity;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;

namespace Microsoft_Identity_Project.Web.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName![0].ToString(), out _);
            if (isDigit)
            {
                errors.Add(new() { Code = "UserNamContainFristLetterDigit", Description = "The first character of the username cannot contain digit character!" });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
