namespace HappiSteps.Contracts.Children;

public sealed record ChildIdentifierResponse(
    string Type,
    string Value,
    DateTime AssignedAt
);
