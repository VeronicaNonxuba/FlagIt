using FlaggingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Data;

public class FlaggingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EstablishmentType> EstablishmentType { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Flagger> Users { get; set; }
    public DbSet<Flagging> Flagging { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flagging>()
            .HasKey(flagging => new { flagging.FlagId, flagging.EstablishmentId, flagging.FlaggedBy });
        modelBuilder.Entity<Flagging>()
            .HasIndex(flagging => new { flagging.FlagId, flagging.EstablishmentId, flagging.FlaggedBy });
        modelBuilder.Entity<Flagging>()
            .HasOne(f => f.Flagger)
            .WithMany(flagger => flagger.Flagging)
            .HasForeignKey(f => f.FlaggedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flagging>()
            .HasOne(f => f.Establishment)
            .WithMany(e => e.Flagging)
            .HasForeignKey(f => f.EstablishmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Flagging>()
            .HasOne(f => f.Flag)
            .WithMany(flag => flag.Flagging)
            .HasForeignKey(f => f.FlagId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Establishment>()
            .HasIndex(e => e.Name)
            .IsUnique();
    }

}
