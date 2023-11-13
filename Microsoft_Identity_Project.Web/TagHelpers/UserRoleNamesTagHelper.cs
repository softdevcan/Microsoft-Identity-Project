using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft_Identity_Project.Core.Models;
using Microsoft_Identity_Project.Repository.Models;

namespace Microsoft_Identity_Project.Web.TagHelpers
{
    public class UserRoleNamesTagHelper:TagHelper
    {
        public string UserId { get; set; } = null!;

        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            var userRoles = await _userManager.GetRolesAsync(user!);

            var stringBuilder = new StringBuilder();

            userRoles.ToList().ForEach(x =>
            {
                stringBuilder.Append(@$"<span class='badge bg-secondary'>{x.ToLower()}</span");
            });
            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
