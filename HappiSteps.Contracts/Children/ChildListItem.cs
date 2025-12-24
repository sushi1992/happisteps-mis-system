namespace HappiSteps.Contracts.Children;

public sealed record ChildListItem(
    Guid ChildId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Status,
    DateOnly? OnRollDate
);
