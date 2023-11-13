using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft_Identity_Project.Core.Models;

namespace Microsoft_Identity_Project.Repository.Models
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

    }
}
