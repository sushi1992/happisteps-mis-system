using HappiSteps.Domain.Children;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.Infrastructure.Persistence;

public class HappiStepsDbContext : DbContext
{
    public DbSet<Child> Children => Set<Child>();

    public HappiStepsDbContext(DbContextOptions<HappiStepsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HappiStepsDbContext).Assembly);
    }
}
