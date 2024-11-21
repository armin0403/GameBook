using GameBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Infrastructure
{
    public class GameBookDbContext : DbContext
    {
        public GameBookDbContext(DbContextOptions<GameBookDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

    }
}
