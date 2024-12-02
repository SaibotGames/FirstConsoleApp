using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContex : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:/Users/user/Documents/GitHub/FirstConsoleApp/EfcRepositories/app.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure foreign key relationship for Comment -> Post
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade); // or Restrict based on your needs

        // Configure foreign key relationship for Comment -> User
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade); // or Restrict based on your needs
    }
}