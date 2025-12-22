using HappiSteps.Domain.Children;

namespace HappiSteps.Application.Common.Interfaces;

public interface IChildRepository
{
    Task AddAsync(Child child, CancellationToken cancellationToken = default);

    // Read-only
    Task<Child?> GetByIdAsync(Guid childId, CancellationToken cancellationToken = default);

    // Mutating use-cases
    Task<Child?> GetTrackedByIdAsync(
        Guid childId,
        CancellationToken cancellationToken = default);
}
