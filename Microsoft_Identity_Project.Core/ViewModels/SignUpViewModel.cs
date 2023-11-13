using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel() 
        { 

        }

        public SignUpViewModel(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }
        [Required(ErrorMessage = "Please note that the User Name field is required!")]
        [Display(Name="User Name :")]
        public string UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email format is wrong!")]
        [Required(ErrorMessage = "Please note that the Email field is required!")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please note that the Phone field is required!")]
        [Display(Name = "Phone :")]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Phone number must be 10 digits and should not start with zero.")]
        public string Phone { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please note that the Password field is required!")]
        [Display(Name = "Password :")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 characters.")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password is not the same!")]
        [Required(ErrorMessage = "Please note that the Password Again field is required!")]
        [Display(Name = "Password Again :")]
        [MinLength(6, ErrorMessage = "Your password must be at least 6 characters.")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
