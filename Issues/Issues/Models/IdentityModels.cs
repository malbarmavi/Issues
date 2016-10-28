using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Issues.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profile { get; set; }

        public DbSet<Company> Company { get; set; }

        public ApplicationDbContext()
            : base("IssuesDataBae", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}