using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieComment> MovieComments { get; set; }

        public DbSet<MovieSession> MovieSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<MovieComment>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MovieSession>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Sessions)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MovieSession>()
                .Property(s => s.Price)
                .HasPrecision(10, 2);
            modelBuilder.Entity<MovieSession>()
                .HasIndex(s => s.StartsAt);
        }
    }
}
