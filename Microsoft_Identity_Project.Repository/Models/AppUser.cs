using Microsoft.AspNetCore.Identity;
using Microsoft_Identity_Project.Core.Models;

namespace Microsoft_Identity_Project.Repository.Models
{
    public class AppUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public int? ConfirmCode { get; set; }
        public void SetBirthDate(DateTime birthDate)
        {
            // Doğrudan UTC olarak ayarla
            BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
        }
    }
}
