using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft_Identity_Project.Core.ViewModels;

namespace Microsoft_Identity_Project.Service.Services
{
    public interface IMemberService
    {
        Task<UserViewModel> GetUserViewModelByUserNameAsync(string userName);

        Task LogoutAsync();

        Task<bool> CheckPasswordAsync(string userName, string password);

        Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string oldPassword, string newPassword);

        Task<UserEditViewModel> GetUserEditViewModelAsync(string userName);
        SelectList GetGenderSelectList();
        Task<(bool, IEnumerable<IdentityError>?)> EditUserAsync(UserEditViewModel request, string userName);
    }
}
