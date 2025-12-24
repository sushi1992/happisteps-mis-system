using System.Threading;
using System.Threading.Tasks;

namespace HappiSteps.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
