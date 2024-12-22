using FlaggingService.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlaggingService.Data;

public class FlaggingDbContext : DbContext
{
    public FlaggingDbContext(DbContextOptions<FlaggingDbContext> options) : base(options)
    {
    }

    public DbSet<EstablishmentType> EstablishmentType { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<Flagger> Users { get; set; }
    public DbSet<Flagging> Flagging { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flagging>().HasKey(u => new { u.FlagId, u.FlaggedBy, u.EstablishmentId });
        modelBuilder.Entity<Flagging>()
                    .HasOne(z => z.Establishment).WithMany(z => z.Flagging).HasForeignKey(z => z.EstablishmentId);
        modelBuilder.Entity<Flagging>()
                    .HasOne(z => z.Flag).WithMany(z => z.Flagging).HasForeignKey(z => z.FlagId);
        modelBuilder.Entity<Flagging>()
                    .HasOne(z => z.Flagger).WithMany(z => z.Flagging).HasForeignKey(z => z.FlaggedBy);
    }

}
