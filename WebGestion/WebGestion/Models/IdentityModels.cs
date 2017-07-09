using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebGestion.Models
{
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUser<int>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public int AppId { get; set; }
        //public Usuarios Usuario { get; set; }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
        int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("GesemEntitiesAuth")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    ///////////////////////////////////////////////////////


    //public class ApplicationGroup
    //{
    //    public ApplicationGroup()
    //    {
    //        this.Id = default(int);
    //        this.ApplicationRoles = new List<ApplicationGroupRole>();
    //        this.ApplicationUsers = new List<ApplicationUserGroup>();
    //    }

    //    public ApplicationGroup(string name)
    //        : this()
    //    {
    //        this.Name = name;
    //    }

    //    public ApplicationGroup(string name, string description)
    //        : this(name)
    //    {
    //        this.Description = description;
    //    }

    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
    //    public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
    //}


    //public class ApplicationUserGroup
    //{
    //    public int ApplicationUserId { get; set; }
    //    public int ApplicationGroupId { get; set; }
    //}

    //public class ApplicationGroupRole
    //{
    //    public int ApplicationGroupId { get; set; }
    //    public int ApplicationRoleId { get; set; }
    //}

}