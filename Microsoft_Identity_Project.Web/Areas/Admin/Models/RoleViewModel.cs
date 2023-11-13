using System.ComponentModel.DataAnnotations;

namespace Microsoft_Identity_Project.Web.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; } = null!;


        [Required(ErrorMessage = "Please note that the Role Name field is required!")]
        [Display(Name = "Role Name :")]
        public string Name { get; set; } = null!;
    }
}
