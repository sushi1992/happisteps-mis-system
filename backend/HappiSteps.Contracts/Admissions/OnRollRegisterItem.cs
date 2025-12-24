namespace HappiSteps.Contracts.Admissions;

public sealed record OnRollRegisterItem(
    Guid ChildId,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string? Upn,
    DateOnly AdmissionDate
);
