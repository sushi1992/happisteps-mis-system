using HappiSteps.Domain.Children;
using HappiSteps.Domain.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.Infrastructure.Persistence;

public class HappiStepsDbContext : DbContext
{
    public HappiStepsDbContext(DbContextOptions<HappiStepsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Child> Children => Set<Child>();
    public DbSet<ChildIdentifier> ChildIdentifiers => Set<ChildIdentifier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --------------------
        // Child
        // --------------------
        modelBuilder.Entity<Child>(builder =>
        {
            builder.HasKey(c => c.ChildId);

            builder.Property(c => c.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(c => c.Identifiers)
                   .WithOne()
                   .HasForeignKey(ci => ci.ChildId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        // --------------------
        // ChildIdentifier
        // --------------------
        modelBuilder.Entity<ChildIdentifier>(builder =>
        {
            // ✅ Composite primary key
            builder.HasKey(ci => new { ci.ChildId, ci.Type });

            builder.Property(ci => ci.Type)
                   .IsRequired();

            builder.Property(ci => ci.Value)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(ci => ci.AssignedAt)
                   .IsRequired();
        });
    }
}
