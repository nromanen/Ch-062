using System.Data.Entity;
using Model.DB;

namespace DAL
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("OnlineExam")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public MainDbContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
