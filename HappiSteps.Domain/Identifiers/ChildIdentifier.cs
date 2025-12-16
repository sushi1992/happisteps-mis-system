namespace HappiSteps.Domain.Identifiers;

public class ChildIdentifier
{
    private ChildIdentifier() { }

    public IdentifierType Type { get; private set; }
    public string Value { get; private set; } = null!;
    public DateTime AssignedAt { get; private set; }

    internal static ChildIdentifier CreateUpn(string value)
    {
        return new ChildIdentifier
        {
            Type = IdentifierType.UPN,
            Value = value.Trim(),
            AssignedAt = DateTime.UtcNow
        };
    }
}
