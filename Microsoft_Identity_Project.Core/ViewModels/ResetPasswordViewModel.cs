using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please note that the Password field is required!")]
        [Display(Name = "New Password :")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]   
        [Compare(nameof(Password), ErrorMessage = "Password is not the same!")]
        [Required(ErrorMessage = "Please note that the Password Again field is required!")]
        [Display(Name = "New Password Again :")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
