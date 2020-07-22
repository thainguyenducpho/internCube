using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CubeSYSVN_Intern_anhthai.Models;

namespace CubeSYSVN_Intern_anhthai.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("anhthaiEntities", throwIfV1Schema: false)
        {
        }


        public virtual DbSet<SVR_BUMON> SVR_BUMON { get; set; }

        public virtual DbSet<SVR_COMPANY> SVR_COMPANY { get; set; }

        public virtual DbSet<SVR_STORE> SVR_STORE { get; set; }

        public virtual DbSet<SVR_USER> SVR_USER { get; set; }

        public virtual DbSet<SVR_VERSION> SVR_VERSION { get; set; }

        public virtual DbSet<SVR_VIEW> SVR_VIEW { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}