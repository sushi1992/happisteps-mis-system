using HappiSteps.Domain.Common;

namespace HappiSteps.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly HappiStepsDbContext _dbContext;

    public UnitOfWork(HappiStepsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
