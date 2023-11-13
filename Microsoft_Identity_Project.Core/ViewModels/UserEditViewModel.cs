using Microsoft.AspNetCore.Http;
using Microsoft_Identity_Project.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Core.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Please note that the User Name field is required!")]
        [Display(Name = "User Name :")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Please note that the First Name field is required!")]
        [Display(Name = "First Name :")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please note that the Last Name field is required!")]
        [Display(Name = "Last Name :")]
        public string LastName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email format is wrong!")]
        [Required(ErrorMessage = "Please note that the Email field is required!")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please note that the Phone field is required!")]
        [Display(Name = "Phone :")]
        [RegularExpression(@"^[1-9]\d{9}$", ErrorMessage = "Phone number must be 10 digits and should not start with zero.")]
        public string Phone { get; set; } = null!;
         
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date :")]
        public DateTime? BirthDate { get; set; }


        [Display(Name = "City :")]
        public string? City{ get; set; }

        [Display(Name = "Profile Picture :")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Gender :")]
        public Gender? Gender { get; set; }
    }
}
