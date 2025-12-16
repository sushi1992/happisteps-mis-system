using HappiSteps.Domain.Children;
using Microsoft.EntityFrameworkCore;

namespace HappiSteps.Infrastructure.Persistence.Repositories;

public sealed class ChildRepository : IChildRepository
{
    private readonly HappiStepsDbContext _dbContext;

    public ChildRepository(HappiStepsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Child child, CancellationToken cancellationToken = default)
    {
        await _dbContext.Children.AddAsync(child, cancellationToken);
    }

    public async Task<Child?> GetByIdAsync(Guid childId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Children
            .FirstOrDefaultAsync(c => c.ChildId == childId, cancellationToken);
    }
}
