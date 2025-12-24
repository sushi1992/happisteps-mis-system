namespace HappiSteps.Contracts.Children;

public record CreateChildRequest(
    Guid OrganisationId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);
