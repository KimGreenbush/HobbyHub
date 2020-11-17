using Microsoft.EntityFrameworkCore;

namespace Csharp_belt.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        // DON'T FORGET TO COME BACK AND CHANGE NAMES
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; } //CHANGE
        public DbSet<UserActivity> UserActivities { get; set; } //CHANGE
    }
}