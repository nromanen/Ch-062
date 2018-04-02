using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.DB;

namespace DAL
{
    public class MainDbContext : IdentityDbContext<User>
    {
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<User> Users { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
