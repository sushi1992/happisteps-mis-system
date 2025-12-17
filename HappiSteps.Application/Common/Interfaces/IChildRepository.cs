using HappiSteps.Domain.Children;

namespace HappiSteps.Application.Common.Interfaces;

public interface IChildRepository
{
    Task AddAsync(Child child, CancellationToken cancellationToken = default);
    Task<Child?> GetByIdAsync(Guid childId, CancellationToken cancellationToken = default);
}
