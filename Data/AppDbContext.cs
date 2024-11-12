using AnimalAdventure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdventure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Image> Images{ get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
