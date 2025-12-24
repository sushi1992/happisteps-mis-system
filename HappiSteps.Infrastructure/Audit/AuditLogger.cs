using System.Text.Json;
using HappiSteps.Application.Common.Interfaces;
using HappiSteps.Infrastructure.Persistence;
using HappiSteps.Domain.Audit;

namespace HappiSteps.Infrastructure.Audit;

public sealed class AuditLogger : IAuditLogger
{
    private readonly HappiStepsDbContext _dbContext;
    private readonly IOrganisationContext _organisation;
    private readonly IUserContext _user;

    public AuditLogger(
        HappiStepsDbContext dbContext,
        IOrganisationContext organisation,
        IUserContext user)
    {
        _dbContext = dbContext;
        _organisation = organisation;
        _user = user;
    }

    public async Task LogAsync(
        string action,
        string entityType,
        Guid entityId,
        object? metadata = null,
        CancellationToken cancellationToken = default)
    {
        var entry = new AuditLogEntry(
            _organisation.OrganisationId,
            _user.UserId,
            action,
            entityType,
            entityId,
            metadata is null
                ? null
                : JsonSerializer.Serialize(metadata)
        );

        await _dbContext.AuditLogEntries.AddAsync(entry, cancellationToken);
    }
}
