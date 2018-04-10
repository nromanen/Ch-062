using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.DB;

namespace DAL
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public DbSet<TestTask> TestTasks { get; set; }
        public DbSet<Course> Courses { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
