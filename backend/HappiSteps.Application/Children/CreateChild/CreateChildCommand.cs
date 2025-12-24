namespace HappiSteps.Application.Children.CreateChild;

public record CreateChildCommand(
    Guid OrganisationId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);
