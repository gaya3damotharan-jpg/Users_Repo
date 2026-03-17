using Microsoft.EntityFrameworkCore;

namespace usersAPI.Model
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John Doe",
                    Age = 30,
                    City = "Chennai",
                    State = "TN",
                    Pin = "600001"
                }
            );
        }
    }
}
