using Microsoft.EntityFrameworkCore;

namespace MVCTaskProject.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTasks> UsersTasks { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }    
       
    }
}
