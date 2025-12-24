using HappiSteps.Domain.Admissions;
using HappiSteps.Domain.Audit;
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
    public DbSet<Admission> Admissions => Set<Admission>();
    public DbSet<AuditLogEntry> AuditLogEntries => Set<AuditLogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Automatically apply all IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(
             typeof(HappiStepsDbContext).Assembly
        );
    }
}
