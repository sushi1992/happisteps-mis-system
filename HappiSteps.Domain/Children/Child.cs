using HappiSteps.Domain.Identifiers;

namespace HappiSteps.Domain.Children;

public class Child
{
    // EF Core requires a parameterless constructor
    private Child() { }

    public Guid ChildId { get; private set; }
    public Guid OrganisationId { get; private set; }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateOnly DateOfBirth { get; private set; }

    public ChildStatus Status { get; private set; }

    private readonly List<ChildIdentifier> _identifiers = [];
    public IReadOnlyCollection<ChildIdentifier> Identifiers => _identifiers.AsReadOnly();

    // ✅ Single, controlled creation path
    public static Child Create(
        Guid organisationId,
        string firstName,
        string lastName,
        DateOnly dateOfBirth)
    {
        if (organisationId == Guid.Empty)
            throw new ArgumentException("OrganisationId is required");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required");

        return new Child
        {
            ChildId = Guid.NewGuid(),
            OrganisationId = organisationId,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            DateOfBirth = dateOfBirth,
            Status = ChildStatus.Applied
        };
    }

    public void ChangeStatus(ChildStatus newStatus)
    {
        Status = newStatus;
    }

    public void AssignUpn(string upn)
    {
        if (string.IsNullOrWhiteSpace(upn))
            throw new ArgumentException("UPN cannot be empty");

        if (_identifiers.Any(i => i.Type == IdentifierType.UPN))
            throw new InvalidOperationException("UPN is already assigned and cannot be changed.");

        _identifiers.Add(
            new ChildIdentifier(
                ChildId,
                IdentifierType.UPN,
                upn.Trim()
            )
        );
    }
}
