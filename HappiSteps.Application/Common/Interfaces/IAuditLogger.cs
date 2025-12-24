namespace HappiSteps.Application.Common.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(
        string action,
        string entityType,
        Guid entityId,
        object? metadata = null,
        CancellationToken cancellationToken = default);
}
