using FlaggingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Data;

public class FlaggingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EstablishmentType> EstablishmentType { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rating>()
            .HasKey(rating => new { rating.FlagId, rating.EstablishmentId, rating.FlaggedBy, rating.FlaggedOn });
        modelBuilder.Entity<Rating>()
            .HasIndex(rating => new { rating.FlagId, rating.EstablishmentId, rating.FlaggedBy, rating.FlaggedOn });
        modelBuilder.Entity<Rating>()
            .HasOne(f => f.User)
            .WithMany(user => user.Rating)
            .HasForeignKey(f => f.FlaggedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rating>()
            .HasOne(f => f.Establishment)
            .WithMany(e => e.Rating)
            .HasForeignKey(f => f.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Rating>()
            .HasOne(f => f.Flag)
            .WithMany(flag => flag.Rating)
            .HasForeignKey(f => f.FlagId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Establishment>()
            .HasIndex(e => e.Name)
            .IsUnique();
    }

}
