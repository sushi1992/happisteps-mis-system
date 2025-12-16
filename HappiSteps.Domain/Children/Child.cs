using HappiSteps.Domain.Identifiers;

namespace HappiSteps.Domain.Children;

public class Child
{
    public Guid ChildId { get; }
    public Guid OrganisationId { get; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; }
    public ChildStatus Status { get; private set; }

    private readonly List<ChildIdentifier> _identifiers = [];
    public IReadOnlyCollection<ChildIdentifier> Identifiers => _identifiers.AsReadOnly();

    public Child(
       Guid organisationId,
       string firstName,
       string lastName,
       DateOnly dateOfBirth)
    {
        ChildId = Guid.NewGuid();
        OrganisationId = organisationId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Status = ChildStatus.Applied;
    }

    public void ChangeStatus(ChildStatus newStatus)
    {
        Status = newStatus;
    }

    public void AssignUpn(string upn)
    {
        if (_identifiers.Any(i => i.Type == IdentifierType.UPN)){
            throw new InvalidOperationException("UPN is already assigned and cannot be changed.");
        }
        _identifiers.Add(new ChildIdentifier(
            IdentifierType.UPN,
            upn
        ));
    }
}
