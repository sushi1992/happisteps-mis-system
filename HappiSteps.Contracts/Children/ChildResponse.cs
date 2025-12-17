namespace HappiSteps.Contracts.Children;

public sealed record ChildResponse(
    Guid ChildId,
    Guid OrganisationId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Status,
    IReadOnlyCollection<ChildIdentifierResponse> Identifiers
);
