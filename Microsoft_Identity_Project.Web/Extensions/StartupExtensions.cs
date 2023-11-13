using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft_Identity_Project.Web.CustomValidations;
using Microsoft_Identity_Project.Core.OptionsModel;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;

namespace Microsoft_Identity_Project.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(1);
            });


            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz12345567890_";
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;


                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
                    
            }).AddUserValidator<UserValidator>().AddPasswordValidator<PasswordValidator>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            //.AddErrorDescriber<LocalizationIdentityErrorDescriber #türkçe hata mesajları için ekle
        }
    }
}
