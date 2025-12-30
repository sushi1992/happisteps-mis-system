using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HappiSteps.Infrastructure.Persistence;

public sealed class HappiStepsDbContextFactory
    : IDesignTimeDbContextFactory<HappiStepsDbContext>
{
    public HappiStepsDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<HappiStepsDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=happisteps;Username=sushi")
            .Options;

        return new HappiStepsDbContext(options);
    }
}
