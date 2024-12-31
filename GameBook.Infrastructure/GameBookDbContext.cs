using GameBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Infrastructure
{
    public class GameBookDbContext : DbContext
    {
        public GameBookDbContext(DbContextOptions<GameBookDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            modelBuilder.Entity<ProfilePlatform>()
                .HasKey(pp => new { pp.ProfileId, pp.PlatformId });

            modelBuilder.Entity<ProfilePlatform>()
                .HasOne(pp => pp.Profile)
                .WithMany(p => p.FavoritePlatform)
                .HasForeignKey(pp => pp.ProfileId);

            modelBuilder.Entity<ProfilePlatform>()
                .HasOne(pp => pp.Platform)
                .WithMany(p => p.ProfilePlatforms)
                .HasForeignKey(pp => pp.PlatformId);

            modelBuilder.Entity<ProfileGenre>()
                .HasKey(pg => new {pg.ProfileId, pg.GenreId });

            modelBuilder.Entity<ProfileGenre>()
                .HasOne(pg => pg.Profile)
                .WithMany(p => p.FavoriteGenre)
                .HasForeignKey(pg => pg.ProfileId);

            modelBuilder.Entity<ProfileGenre>()
                .HasOne(pg => pg.Genre)
                .WithMany(p => p.ProfileGenres)
                .HasForeignKey(pg => pg.GenreId);

            modelBuilder.Entity<GamePlatform>()
                .HasKey(gp => new { gp.GameId, gp.PlatformId});

            modelBuilder.Entity<GamePlatform>()
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlatforms)
                .HasForeignKey(gp => gp.GameId);

            modelBuilder.Entity<GamePlatform>()
                .HasOne(gp => gp.Platform)
                .WithMany(p => p.GamePlatforms)
                .HasForeignKey(gp => gp.PlatformId);

            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new {gg.GameId, gg.GenreId});

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId);

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreId);


        }
    }
}
