using System.Linq;
using Microsoft.AspNetCore.Identity;
using Model;
using Model.DB;

namespace DAL.Seed
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MainDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(
            MainDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
                string userAdmin = "admin@gmail.com";
                string passwordAdmin = "Admin_123";
                userManager.CreateAsync(new User { UserName = userAdmin, Email = userAdmin, EmailConfirmed = true }, passwordAdmin).Wait();
                var t = userManager.FindByNameAsync(userAdmin);
                userManager.AddToRoleAsync(t.Result, "Administrator").Wait();
            }

            if (!context.Roles.Any(r => r.Name == "Teacher"))
            {
                roleManager.CreateAsync(new IdentityRole("Teacher")).Wait();
                string userTeacher = "teacher@gmail.com";
                string passwordTeacher = "Teacher_123";
                userManager.CreateAsync(new User { UserName = userTeacher, Email = userTeacher, EmailConfirmed = true }, passwordTeacher).Wait();
                var t = userManager.FindByNameAsync(userTeacher);
                userManager.AddToRoleAsync(t.Result, "Teacher").Wait();
            }
        }
    }
}
