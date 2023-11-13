using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Email format is wrong!")]
        [Required(ErrorMessage = "Please note that the Email field is required!")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;
    }
}
