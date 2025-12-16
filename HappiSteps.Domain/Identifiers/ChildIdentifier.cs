namespace HappiSteps.Domain.Identifiers;

public class ChildIdentifier
{
    public IdentifierType Type { get; }
    public string Value { get; }
    public DateTime AssignedAt { get; }

    internal ChildIdentifier(IdentifierType type, string value)
    {
        Type = type;
        Value = value;
        AssignedAt = DateTime.UtcNow;
    }
}
