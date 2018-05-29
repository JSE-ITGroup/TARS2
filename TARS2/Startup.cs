using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TARS2.Models;

[assembly: OwinStartupAttribute(typeof(TARS2.Startup))]
namespace TARS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "jseitgroup@jamstockex.com";

                string userPWD = "JSEL0ca!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Riccalya.Robb";
                user.Email = "Riccalya.Robb@jamstockex.com";
                string UserPWD = "P@ssw0rd7";

                var chkUser = UserManager.Create(user, UserPWD);

                if(chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Manager");
                }

            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Supervisor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Sheneka.Robinson";
                user.Email = "Sheneka.Robinson@jamstockex.com";
                string UserPWD = "P@ssw0rd";

                var chkUser = UserManager.Create(user, UserPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Manager");
                }
            }

            // creating Creating Clerk role    
            if (!roleManager.RoleExists("Clerk"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Clerk";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "Lennox.Irving";
                user.Email = "Lennox.Irving@jamstockex.com";
                string userPWD = "P@ssw0rd";

                var user2 = new ApplicationUser();
                user2.UserName = "Travion.Lightbody";
                user2.Email = "Travion.Lightbody@jamstockex.com";
                string userPWD2 = "P@ssw0rd";

                var chkUser = UserManager.Create(user, userPWD);
                var chkUser2 = UserManager.Create(user2, userPWD2);

                //Add default User to Role Clerk
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Clerk");

                }

                if (chkUser2.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user2.Id, "Clerk");

                }

            }
        }
    }
}
