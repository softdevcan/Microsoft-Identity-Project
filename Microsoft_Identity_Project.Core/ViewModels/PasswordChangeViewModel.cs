using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class PasswordChangeViewModel
    {

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please note that the Password Again field is required!")]
        [Display(Name = "Old Password :")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 characters.")]
        public string PasswordOld { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please note that the Password Again field is required!")]
        [Display(Name = "New Password :")]
        public string PasswordNew { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Password is not the same!")]
        [Required(ErrorMessage = "Please note that the Password Again field is required!")]
        [Display(Name = "New Password Again :")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
