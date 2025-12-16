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
        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(c => c.ChildId);

            entity.Property(c => c.FirstName).IsRequired();
            entity.Property(c => c.LastName).IsRequired();

            entity.HasMany<ChildIdentifier>("_identifiers")
                  .WithOne()
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
