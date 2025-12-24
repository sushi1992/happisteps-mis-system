namespace HappiSteps.Domain.Identifiers;

public class ChildIdentifier
{
    private ChildIdentifier() { } // EF

    public Guid ChildId { get; private set; }
    public IdentifierType Type { get; private set; }
    public string Value { get; private set; } = null!;
    public DateTime AssignedAt { get; private set; }

    internal ChildIdentifier(
        Guid childId,
        IdentifierType type,
        string value)
    {
        ChildId = childId;
        Type = type;
        Value = value;
        AssignedAt = DateTime.UtcNow;
    }
}
