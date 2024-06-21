using basic.Models.auth;
using Microsoft.EntityFrameworkCore;

namespace basic.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }
    }
}