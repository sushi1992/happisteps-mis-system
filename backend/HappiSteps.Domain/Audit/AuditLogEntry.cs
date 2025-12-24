namespace HappiSteps.Domain.Audit;

public sealed class AuditLogEntry
{
    private AuditLogEntry() { } // EF

    public Guid AuditLogEntryId { get; private set; }
    public Guid OrganisationId { get; private set; }
    public Guid UserId { get; private set; }

    public string Action { get; private set; } = null!;
    public string EntityType { get; private set; } = null!;
    public Guid EntityId { get; private set; }

    public DateTime OccurredAtUtc { get; private set; }

    public string? MetadataJson { get; private set; }

    public AuditLogEntry(
        Guid organisationId,
        Guid userId,
        string action,
        string entityType,
        Guid entityId,
        string? metadataJson = null)
    {
        AuditLogEntryId = Guid.NewGuid();
        OrganisationId = organisationId;
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        MetadataJson = metadataJson;
        OccurredAtUtc = DateTime.UtcNow;
    }
}
