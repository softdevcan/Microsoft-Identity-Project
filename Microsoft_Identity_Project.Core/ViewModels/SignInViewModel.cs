using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel() { }
        public SignInViewModel(string? email, string? password)
        {
            Email = email!;
            Password = password!;
        }

        [EmailAddress(ErrorMessage = "Email format is wrong!")]
        [Required(ErrorMessage = "Please note that the Email field is required!")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please note that the Password field is required!")]
        [Display(Name = "Password :")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
